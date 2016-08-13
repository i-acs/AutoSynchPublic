using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Net;

namespace Tools
{
    public class FileTransfer
    {
        public class Server
        {
            private TcpComm.Server server;
            private String currentFolder;
            private ConcurrentDictionary<String, Operation> operations = new ConcurrentDictionary<String, Operation>();
            private CallBack callback;

            public delegate void CallBack(String msg);

            private class Operation
            {
                public String sessionId;
                public String filePath;
                public String sessionName;
                public String dataStreamId;
                public long length;
                public DateTime lastWritten;

                private FileInfo thisFile;
                private FileStream fs;
                private DateTime lastbytesIn;
                private bool running;
                private bool complete;
                private TcpComm.Utilities.TcpStream dataStream;
                private List<BytesIn> bytesIn   = new List<BytesIn>();
                private Object bytesLocker      = new Object();

                private class BytesIn
                {
                    public Byte[] bytesIn;
                    public int numToWrite;
                }

                public Operation(String filePath, String sessionId, long fileLength, DateTime lastWritten, ref TcpComm.Utilities.TcpStream ds)
                {
                    this.filePath       = filePath;
                    this.sessionId      = sessionId;
                    this.sessionName    = Regex.Split(sessionId, @"\\")[0];
                    this.dataStreamId   = Regex.Split(sessionId, @"\\")[1];
                    this.length         = fileLength;
                    this.lastWritten    = lastWritten;
                    this.dataStream     = ds;

                    complete            = false;

                    String thisFolder = Path.GetDirectoryName(filePath);

                    if (File.Exists(filePath))
                    {
                        thisFile = new FileInfo(filePath);
                        if (lastWritten.Equals(thisFile.LastWriteTime) && length == thisFile.Length)
                        {
                            throw new Exception("This file (" + Path.GetFileName(filePath) + ") has not been modified since the last transfer attempt.");
                        }
                    }

                    try
                    {
                        if (!Directory.Exists(thisFolder))
                        {
                            Directory.CreateDirectory(thisFolder);
                        }

                        fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                    running = true;
                    lastbytesIn = DateTime.Now;

                    new Thread(() =>
                    {
                        while (running)
                        {
                            if (DateTime.Now > lastbytesIn.AddSeconds(15) || dataStream.State == TcpComm.Utilities.TcpStream.StreamState.Closed)
                            {
                                break;
                            }

                            Thread.Sleep(1);

                            if (bytesIn.Count > 0)
                            {
                                while (bytesIn.Count > 0 && running)
                                {
                                    try
                                    {
                                        lock (bytesLocker)
                                        {
                                            fs.Write(bytesIn[0].bytesIn, 0, bytesIn[0].numToWrite);
                                            bytesIn.RemoveAt(0);
                                            lastbytesIn = DateTime.Now;
                                        }
                                    }
                                    catch (Exception) { running = false; }
                                }
                            }
                        }

                        if (!complete) { Abort(); }
                    }).Start();
                }

                public bool Write(ref byte[] buffer, int numberToWrite, ref String errMsg)
                {
                    // We have a 5 packet buffer...
                    while (bytesIn.Count > 4 && running)
                    {
                        Thread.Sleep(1);
                    }

                    if (!running) { return false; }

                    lock (bytesLocker)
                    {
                        bytesIn.Add(new BytesIn { bytesIn = (Byte[])buffer.Clone(), numToWrite = numberToWrite });
                        lastbytesIn = DateTime.Now;
                    }

                    return true;
                }

                public bool Close(long thisFileSize)
                {
                    bool retVal = false;

                    while (bytesIn.Count > 0 && running)
                    {
                        Thread.Sleep(1);
                    }

                    try
                    {
                        fs.Close();
                    }
                    catch (Exception) { }

                    try
                    {
                        thisFile                = new FileInfo(filePath);
                        thisFile.LastWriteTime  = lastWritten;
                        thisFile.CreationTime   = lastWritten;
                    }
                    catch (Exception) { }

                    try
                    {
                        retVal = new FileInfo(filePath).Length == thisFileSize ? true : false;
                    }
                    catch (Exception) { }

                    complete                = true;
                    running                 = false;
                    return retVal;
                }

                public void Abort()
                {
                    try
                    {
                        fs.Close();
                    }
                    catch (Exception) { }

                    try
                    {
                        File.Delete(filePath);
                    }
                    catch (Exception) { }

                    complete    = false;
                    running     = false;
                }
            }

            public bool GetLocalIpaddresses(ref List<IPAddress> addresses, ref String errMsg)
            {
                try
                {
                    TcpComm.Utilities.GetLocalIpAddress(ref addresses, ref errMsg);
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                    return false;
                }

                if (addresses == null || addresses.Count == 0)
                {
                    errMsg = "There are no available ip addresses.";
                    return false;
                }

                return true;
            }

            private class PacketData
            {
                public byte[] bytes;
                public int numBytes;
                public String sId;
                public String _tag;
            }

            private void DataHandler(Object pd)
            {
                PacketData data         = (PacketData)pd;
                byte[] buffer           = data.bytes;
                int numBytesContained   = data.numBytes;
                String sessionId        = data.sId;
                String errMsg           = "";

                if (buffer == null) { callback(((data.sId != "") ? (data.sId + ": ") : "") + data._tag); return; }

                if (numBytesContained < 1000)
                {
                    String msg = TcpComm.Utilities.BytesToString(buffer, numBytesContained);

                    if (msg.StartsWith("<command="))
                    {
                        if (msg.StartsWith("<command=kill>"))
                        {
                            msg = msg.Replace("<command=kill>", "");
                            Operation o = null;

                            foreach (KeyValuePair<String, Operation> op in operations)
                            {
                                if (op.Value.dataStreamId.Equals(msg))
                                {
                                    o = op.Value;
                                    break;
                                }
                            }

                            if (o != null)
                            {
                                o.Close(0);
                            }
                            return;
                        }

                        if (msg.StartsWith("<command=setpath>"))
                        {
                            msg = msg.Replace("<command=setpath>", "");
                            currentFolder = msg;
                        }

                        if (msg.StartsWith("<command=new>"))
                        {
                            Operation o             = null;
                            String thisPath         = "";
                            String thisTargetFolder = "";
                            long thisLength         = 0;
                            DateTime lastWriteTime  = DateTime.Parse("1/1/1970");

                            if (msg.Contains("<length>"))
                            {
                                thisPath = Regex.Split(msg, "<length>")[1];
                                thisPath = Regex.Split(thisPath, @"</length>")[0];

                                try
                                {
                                    thisLength = long.Parse(thisPath);
                                    thisPath = "";
                                }
                                catch (Exception)
                                {
                                    server.SendText("new:failed: Could not read the file length from the create new command.",
                                        Regex.Split(sessionId, @"\\")[0], Regex.Split(sessionId, @"\\")[1], ref errMsg);
                                    return;
                                }
                            }

                            if (msg.Contains("<targetfolder>"))
                            {
                                thisTargetFolder = Regex.Split(msg, "<targetfolder>")[1];
                                thisTargetFolder = Regex.Split(thisTargetFolder, @"</targetfolder>")[0];

                                if (!Directory.Exists(thisTargetFolder))
                                {
                                    server.SendText("new:failed: The target folder does not exist on the server.",
                                        Regex.Split(sessionId, @"\\")[0], Regex.Split(sessionId, @"\\")[1], ref errMsg);
                                    return;
                                }
                            }
                            else
                            {
                                thisTargetFolder = currentFolder;
                            }

                            if (msg.Contains("<date>"))
                            {
                                thisPath = Regex.Split(msg, "<date>")[1];
                                thisPath = Regex.Split(thisPath, @"</date>")[0];

                                try
                                {
                                    lastWriteTime = DateTime.Parse(thisPath);
                                    thisPath = "";
                                }
                                catch (Exception)
                                {
                                    server.SendText("new:failed: Could not read the last written time from the create new command.",
                                        Regex.Split(sessionId, @"\\")[0], Regex.Split(sessionId, @"\\")[1], ref errMsg);
                                    return;
                                }
                            }

                            if (msg.Contains("<path>"))
                            {
                                thisPath = Regex.Split(msg, "<path>")[1];
                                thisPath = Regex.Split(thisPath, @"</path>")[0];
                            }

                            if (operations.TryRemove(sessionId, out o))
                            {
                                if (o != null)
                                {
                                    try { o.Abort(); }
                                    catch (Exception) { }
                                    o = null;
                                }
                            }

                            if (o != null)
                            {
                                try { o.Abort(); }
                                catch (Exception) { }
                                o = null;
                            }

                            // Check to make sure this transfer isn't happening in another datastream:
                            foreach (KeyValuePair<String, Operation> op in operations)
                            {
                                // If we ARE:
                                if (op.Value.filePath.ToLower().Equals(Path.Combine(thisTargetFolder, thisPath.TrimStart(new char[] { '\\' })).ToLower()))
                                {
                                    // Remove that operation from the collection,
                                    operations.TryRemove(op.Key, out o);
                                    // and Abort it.
                                    op.Value.Abort();

                                    // Set our operation obkect to null
                                    o = null;

                                    // And exit the iterator.
                                    break;
                                }
                            }

                            try
                            {
                                TcpComm.Utilities.TcpStream thisStream = null;
                                TcpComm.Server.Session thisSession = null;

                                if (server.GetSession(ref thisSession, Regex.Split(sessionId, @"\\")[0]))
                                {
                                    if (thisSession.dataStreamCollection.TryGetValue(Regex.Split(sessionId, @"\\")[1], out thisStream))
                                    {
                                        o = new Operation(Path.Combine(thisTargetFolder, thisPath.TrimStart(new char[] { '\\' })), sessionId, thisLength, lastWriteTime, ref thisStream);

                                        if (operations.TryAdd(sessionId, o))
                                        {
                                            server.SendText("new:ok", o.sessionName, o.dataStreamId, ref errMsg);
                                        }
                                        else
                                        {
                                            server.SendText("new:failed: Could not add this operation to the collection.", Regex.Split(sessionId, @"\\")[0], Regex.Split(sessionId, @"\\")[1], ref errMsg);
                                        }
                                    }
                                }
                                else
                                {
                                    server.SendText("new:failed: The session could not be retrieved.", Regex.Split(sessionId, @"\\")[1], ref errMsg);
                                }
                            }
                            catch (Exception ex)
                            {
                                server.SendText("new:failed:" + ex.Message, Regex.Split(sessionId, @"\\")[0], Regex.Split(sessionId, @"\\")[1], ref errMsg);
                            }
                        }

                        if (msg.StartsWith("<command=complete>"))
                        {
                            Operation o;
                            long thisFileSize = -1;

                            msg = msg.Replace("<command=complete>", "");

                            long.TryParse(msg, out thisFileSize);

                            if (operations.TryRemove(sessionId, out o))
                            {
                                if (o.Close(thisFileSize))
                                {
                                    server.SendText("complete:ok", o.sessionName, o.dataStreamId, ref errMsg);
                                }
                                else
                                {
                                    server.SendText("complete:failed", o.sessionName, o.dataStreamId, ref errMsg);
                                }
                            }
                            else
                            {
                                server.SendText("complete:failed", Regex.Split(sessionId, @"\\")[0], Regex.Split(sessionId, @"\\")[1], ref errMsg);
                            }
                        }

                        if (msg.StartsWith("<command=abort>"))
                        {
                            Operation o;

                            if (operations.TryRemove(sessionId, out o))
                            {
                                o.Abort();
                                server.SendText("complete:ok", o.sessionName, o.dataStreamId, ref errMsg);
                            }
                        }

                    }
                    else
                    {
                        // These are just bytes of the file: write them.
                        WriteBytes(sessionId, ref buffer, numBytesContained);
                    }
                }
                else
                {
                    // These are just bytes of the file: write them.
                    WriteBytes(sessionId, ref buffer, numBytesContained);
                }
            }

            //private Object commandLocker = new Object();
            public Server(CallBack cb)
            {
                callback = cb;
                server = new TcpComm.Server((String tag, byte[] buffer, int numBytesContained, String sessionId) =>
                {
                    PacketData p = new PacketData { numBytes = numBytesContained, sId = sessionId, _tag = tag, bytes = (buffer == null) ? null : (Byte[])buffer.Clone() };
                    ThreadPool.QueueUserWorkItem(DataHandler, p);
                });
            }

            public bool Start(UInt16 listenPort, System.Net.IPAddress serverIp, ref String errMsg)
            {
                bool retVal = false;

                try
                {
                    retVal = server.Start(listenPort, serverIp, ref errMsg);
                }
                catch (Exception ex)
                {
                    errMsg = ex.Message;
                    return false;
                }

                return retVal;
            }

            public void Close()
            {   
                server.Close();

                foreach ( KeyValuePair<String, Operation> op in operations)
                {
                    op.Value.Abort();
                }
            }

            private void WriteBytes(String sessionId, ref byte[] buffer, int numToWrite)
            {
                Operation o;
                String errMsg = "";

                if (operations.TryGetValue(sessionId, out o))
                {
                    o.Write(ref buffer, numToWrite, ref errMsg);
                    server.SendText("written:" + numToWrite.ToString(), o.sessionName, o.dataStreamId, ref errMsg);
                }
                else
                {
                    server.SendText("writefailed:" + " No operation with this ID currently exists.", Regex.Split(sessionId, @"\\")[0], Regex.Split(sessionId, @"\\")[1], ref errMsg);
                }
            }
        }

        public class Client
        {
            private TcpComm.Client client;
            private String ipAddress;
            private ushort port;
            private bool running = false;

            public delegate void CallBack(String msg);

            public bool Connected()
            {
                try
                {
                    if (client.State == TcpComm.Client.ClientState.Running)
                    {
                        return true;
                    }
                }
                catch (Exception) { }

                return false;
            }

            public Client(ushort port, String ipAddress, String id = "Transfer Session")
            {
                this.port       = port;
                this.ipAddress  = ipAddress;

                client = new TcpComm.Client(delegate(string tag, byte[] buffer, int numBytesContained, string sessionId)
                {
                    // Do nothing here!
                }, id);
            }

            public void Connect()
            {
                String errMsg = "";
                if (!client.Connect(port, ipAddress, ref errMsg))
                {
                    throw new Exception(errMsg);
                }
            }

            public void SetRemoteFolder(String thePath)
            {
                String errMsg = "";
                client.SendText("<command=setpath>" + thePath, ref errMsg);
            }

            public void Close()
            {
                if (running)
                {
                    running = false;
                    Thread.Sleep(500);
                }

                foreach (Transfer transfer in fileTransfers)
                {
                    transfer.Close();
                }

                try
                {
                    client.Close();
                }
                catch (Exception) { }
            }

            public class Transfer
            {
                private int bufferSize                      = 65535 * 2;
                private byte[] buffer;
                private int read                            = 0;
                private long bytesWritten                   = 0;
                private bool complete                       = false;
                private bool closeDatastream                = false;
                private bool creatNewSucceeded              = false;
                private bool createNewFailed                = false;
                private bool connectionLost                 = false;
                private bool running                        = false;
                private DateTime packetTimeout              = DateTime.Now;
                private DateTime lastWritten;
                private FileStream fs                       = null;
                private Object writtenLock                  = new Object();
                private TcpComm.Utilities.TcpStream thisDataStream  = null;
                private String filePath                     = "";
                private String baseFolder                   = "";
                private String targetFolder                 = "";
                private String streamName                   = "";
                private TcpComm.Client client               = null;
                private long totalBytesWritten              = 0;

                public long length                          = 0;
                public Client.CallBack callback;
                public String errMsg                        = "";

                public Transfer(Client.CallBack callback, TcpComm.Client _client)
                {
                    buffer          = new byte[bufferSize];
                    this.callback   = callback;
                    client          = _client;
                }

                public bool IsComplete()
                {
                    return complete;
                }

                public long BytesWritten()
                {
                    long written = 0;

                    lock (writtenLock) { written = totalBytesWritten; }

                    return written;
                }

                private void CloseDatastream(String name)
                {
                    String msg = "";

                    try
                    {
                        client.CloseDataStream(name, ref msg);
                    }
                    catch (Exception) { }

                    Thread.Sleep(250);
                }

                public bool Send(String _filePath, String _baseFolder, String _targetFolder, String _streamName, int mbPerSec = 1000, bool block = false)
                {
                    this.filePath       = _filePath;
                    this.baseFolder     = _baseFolder;
                    this.targetFolder   = _targetFolder;
                    this.streamName     = _streamName;
                    this.connectionLost = false;
                    bool retVal         = true;
                    bool transferOk     = true;
                    bool writeFailed    = false;
                    FileInfo thisFileinfo;
                    int lastRead        = 0;

                    bytesWritten        = 0;
                    totalBytesWritten   = 0;
                    complete            = false;
                    errMsg              = "";

                    if (File.Exists(filePath))
                    {
                        thisFileinfo    = new FileInfo(filePath);
                        lastWritten     = thisFileinfo.LastWriteTime;
                        length          = thisFileinfo.Length;
                    } 
                    else 
                    {
                        errMsg  = "Transfer failed: The Source file does not exist.";
                        retVal  = false;
                        running = false;
                        return false;
                    }

                    createNewFailed     = false;
                    creatNewSucceeded   = false;

                    if (running)
                    {
                        errMsg = "Transfer failed: A file is already being sent by this Transfer object.";
                        return false;
                    }

                    running = true;

                    new Thread(() =>
                    {
                        // Convert this to long form:
                        mbPerSec = (mbPerSec * 1000000);

                        // Convert it to bytes.
                        mbPerSec = (mbPerSec / 8);

                        // We don't want to send more then mbPerSec bytes a second, so
                        // If buffersize if larger then it may happen. Let's make sure it
                        // doesn't.
                        if (bufferSize > mbPerSec)
                        {
                            bufferSize = mbPerSec;
                        }

                        try
                        {   // Create the reader filestream:
                            fs = new FileStream(filePath, FileMode.Open);
                        }
                        catch (Exception ex)
                        {
                            errMsg  = "file aborted: Could not access the source file: " + Path.GetFileName(filePath) + ". The error returned is: " + ex.Message;
                            retVal  = false;
                            running = false;
                            return;
                        }

                        // Define the cleanup function (it will be called from several different places).
                        Action<String> cleanUp = (String msg) =>
                        {
                            if (closeDatastream)
                            {
                                CloseDatastream(streamName);
                            }

                            try
                            {
                                // We're finished with the source file, so close it.
                                fs.Close();
                            }
                            catch (Exception) { }

                            callback(msg);
                            running = false;
                        };

                        // Make sure we haven't been stopped before we even begin:
                        if (!client.Running())
                        {
                            running = false;
                            cleanUp("file aborted - FileTransfer not running! (" + Path.GetFileName(filePath) + ")");
                            return;
                        }

                        // Define our DataStream name:
                        if (streamName.Equals("")) { streamName = filePath.Replace(@"\", "-"); closeDatastream = true; }

                        Func<String, bool, bool> GetDataStream = (String thisStreamName, bool killExisting) =>
                        {
                            // Are we creating a new datastream for every transfer (slower), or reusing one over and over (faster)
                            if (closeDatastream || killExisting)
                            {
                                CloseDatastream(thisStreamName);
                            }

                            // Get the tcp DataStream
                            if (!client.GetDataStream(thisStreamName, ref thisDataStream, ref errMsg))
                            {
                                if (!client.CreateDataStream(thisStreamName, ref errMsg))
                                {
                                    errMsg  = "file aborted (connection lost)! ( Could not create DataStream '" + streamName + "' )";
                                    CloseDatastream(thisStreamName);
                                    retVal  = false;
                                    running = false;
                                    return false; ;
                                }

                                if (!client.GetDataStream(thisStreamName, ref thisDataStream, ref errMsg))
                                {
                                    errMsg  = "file aborted (connection lost)! ( Could not get DataStream '" + streamName + "' )";
                                    CloseDatastream(thisStreamName);
                                    retVal  = false;
                                    running = false;
                                    return false;
                                }
                            }

                            return true;
                        };

                        if (!GetDataStream(streamName, false))
                        {
                            retVal  = false;
                            cleanUp(errMsg);
                            return;
                        }

                        // Re-define the DataStream's callback here so we can get control messages from it:
                        thisDataStream.callback = delegate(string msg, byte[] dataStreamBuffer, int numBytesContained, string sessionId)
                        {
                            if (dataStreamBuffer != null)
                            {
                                String thisMsg = TcpComm.Utilities.BytesToString(dataStreamBuffer, numBytesContained);

                                if (thisMsg == "complete:ok")
                                {
                                    complete = true;
                                }

                                if (thisMsg == "complete:failed")
                                {
                                    transferOk  = false;
                                    complete    = true;
                                }

                                if (thisMsg == "new:ok")
                                {
                                    creatNewSucceeded = true;
                                }

                                if (thisMsg.StartsWith("new:failed:"))
                                {
                                    errMsg = thisMsg.Replace("new:failed:", "Transfer Failed: ");
                                    createNewFailed = true;
                                }

                                if (thisMsg.StartsWith("writefailed:"))
                                {
                                    writeFailed = true;
                                }

                                if (thisMsg.StartsWith("written:"))
                                {
                                    thisMsg = thisMsg.Replace("written:", "");

                                    try
                                    {
                                        bytesWritten = int.Parse(thisMsg);
                                    }
                                    catch (Exception)
                                    {
                                        retVal = false;
                                        cleanUp("Transfer Failed: Could not read (parse) the number of bytes written by the server. Communication error.");
                                        return;
                                    }
                                }
                            }
                        };

                        // Define a control message of our own here:
                        String newCommand = "<command=new><path>" + filePath.Replace(baseFolder, "") + @"</path>";
                        if (File.Exists(filePath))
                        {
                            newCommand += "<length>" + length.ToString() + @"</length><date>" + lastWritten.ToString() + @"</date><targetfolder>" + targetFolder + "</targetfolder>";
                        }
                        else
                        {
                            retVal = false;
                            cleanUp("Transfer failed: The source file does not exist.");
                            return;
                        }

                        // Send our "new file transfer" control message to the file transfer server here:
                        if (!client.SendText(newCommand, streamName, ref errMsg))
                        {
                            errMsg = "file aborted (connection lost)! Could not communicate with server: " + errMsg;

                            try
                            {
                                fs.Close();
                            }
                            catch (Exception) { }

                            retVal = false;
                            cleanUp(errMsg);
                            return;
                        }

                        // Wait for the server's response for up to 3 second:
                        new Tools.WaitTimeout(3000, () => { return (createNewFailed || creatNewSucceeded); }).Wait();

                        // Did the server have a problem with our proposed transfer (or did it fail to respond within 3 seconds)?
                        if (createNewFailed)
                        {
                            try
                            {
                                fs.Close();
                            }
                            catch (Exception) { }

                            retVal = false;
                            cleanUp(errMsg);

                            return;
                        }

                        // The transfer has been accepted - on to the good stuff:
                        try
                        {   // Get the first chunk of file bytes that will be sent:
                            read        = fs.Read(buffer, 0, bufferSize);
                            lastRead    = read;
                        }
                        catch (Exception ex)
                        {
                            errMsg = "Transfer failed: Can not read from source file: " + ex.Message;
                            retVal = false;
                            cleanUp(errMsg);
                            return;
                        }

                        if (mbPerSec > 0 && bufferSize > (mbPerSec / 4)) { bufferSize = mbPerSec / 4; }

                        DateTime timeout    = DateTime.Now.AddSeconds(1);
                        int sentThisSec     = 0;

                        // Begin sending file bytes:
                        while (read > 0 && running && client.Running())
                        {
                            // Respect the throttle value here:
                            if (mbPerSec > 0)
                            {
                                if (sentThisSec >= mbPerSec && DateTime.Now < timeout)
                                {
                                    while (DateTime.Now < timeout) { Thread.Sleep(1); }
                                }

                                if (DateTime.Now >= timeout) { timeout = DateTime.Now.AddSeconds(1); sentThisSec = 0; }
                            }

                            // Actually send the bytes we've read here:
                            bytesWritten = 0;
                            if (!client.SendArray(buffer, read, streamName, ref errMsg))
                            {
                                errMsg  = "file aborted (connection lost)! Can not communicate with file transfer server: " + errMsg;
                                retVal  = false;
                                cleanUp(errMsg);
                                return;
                            }

                            sentThisSec += read;
                            totalBytesWritten += read;

                            try
                            {   // Get some more bytes:
                                read = fs.Read(buffer, 0, bufferSize);
                            }
                            catch (Exception ex)
                            {
                                errMsg  = "file aborted (connection lost)! Can not read from source file: " + ex.Message;
                                retVal  = false;
                                cleanUp(errMsg);
                                return;
                            }

                            // Wait for the server to acknolage the last chunk of bytes read and sent:
                            packetTimeout = DateTime.Now.AddSeconds(15);
                            while (bytesWritten != lastRead && running && client.Running())
                            {
                                Thread.Sleep(1);
                                if (DateTime.Now > packetTimeout)
                                {
                                    // We've lost connection for some reason. Bail:
                                    connectionLost = true;
                                    break;
                                }

                                if (writeFailed) { connectionLost = true; break; }
                            }

                            if (connectionLost) { break; }

                            lastRead = read;
                        }

                        try
                        {
                            // We're finished with the source file, so close it.
                            fs.Close();
                        }
                        catch (Exception) { }

                        if (writeFailed)
                        {
                            errMsg = "file aborted (connection lost)! A transfer operation with this ID could not be found in the server. ";
                            retVal = false;
                            cleanUp(errMsg);
                            return;
                        }

                        if (totalBytesWritten < length && !client.Running())
                        {
                            errMsg = "file aborted (connection lost)! Can not communicate with file transfer server: " + errMsg;
                            retVal = false;
                            cleanUp(errMsg);
                            return;
                        }

                        // Finishing up: has the client disconnected?
                        if (!client.Running())
                        {
                            running = false;
                        }

                        // Did we loose connection?
                        if (connectionLost)
                        {
                            // Attempt to send this no matter what:
                            CloseDatastream(streamName);

                            errMsg = "file aborted: connection lost during transfer! (" + Path.GetFileName(filePath) + ")";
                            retVal  = false;
                            cleanUp(errMsg);
                            return;
                        }

                        // Are we being asked to shut down?
                        if (!running)
                        {
                            if (!client.SendArray(TcpComm.Utilities.StringToBytes("<command=abort>"), streamName, ref errMsg))
                            {
                                //running = false;
                            }

                            CloseDatastream(streamName);

                            errMsg = "file aborted! (" + Path.GetFileName(filePath) + ")";
                            retVal = false;
                            cleanUp(errMsg);
                            return;
                        }

                        if (!client.SendArray(TcpComm.Utilities.StringToBytes("<command=complete>" + length.ToString()), streamName, ref errMsg))
                        {
                            errMsg = "file aborted! (Could not send file complete message)";
                            retVal = false;
                            cleanUp(errMsg);
                            return;
                        }

                        // Wait for the server to acknolage that we're finished sending bytes for this file:
                        new WaitTimeout(14000, () => { return (complete || !running); }).Wait();

                        if (!complete)
                        {
                            errMsg = "file aborted (connection lost)! (" + Path.GetFileName(filePath) + ")";
                            CloseDatastream(streamName);
                            retVal = false;
                            cleanUp(errMsg);
                            return;
                        }

                        if (!transferOk)
                        {
                            errMsg = "file aborted (target corrupt)! (" + Path.GetFileName(filePath) + ")";
                            CloseDatastream(streamName);
                            retVal = false;
                            cleanUp(errMsg);
                            return;
                        }

                        // Clean up and exit.
                        retVal = true;
                        cleanUp("file Complete: " + Path.GetFileName(filePath));

                    }).Start();

                    if (block)
                    {
                        while (running)
                        {
                            Thread.Sleep(1);
                        }
                    }

                    return retVal;
                }

                public void Close()
                {
                    running = false;
                }
            }

            private Object transferlocker = new Object();
            public List<Transfer> fileTransfers = new List<Transfer>();

            public Transfer CreateNewTransfer(Client.CallBack callback)
            {
                Transfer thisTransfer = new Transfer(callback, client);

                lock (transferlocker)
                {
                    fileTransfers.Add(thisTransfer);
                }

                return thisTransfer;
            }
        }
    }
}
