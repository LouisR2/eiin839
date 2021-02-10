using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BasicServerHTTPlistener
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("A more recent Windows version is required to use the HttpListener class.");
                return;
            }
 
 
            // Create a listener.
            HttpListener listener = new HttpListener();

            // Add the prefixes.
            if (args.Length != 0)
            {
                foreach (string s in args)
                {
                    listener.Prefixes.Add(s);
                    // don't forget to authorize access to the TCP/IP addresses localhost:xxxx and localhost:yyyy 
                    // with netsh http add urlacl url=http://localhost:xxxx/ user="Tout le monde"
                    // and netsh http add urlacl url=http://localhost:yyyy/ user="Tout le monde"
                    // user="Tout le monde" is language dependent, use user=Everyone in english 

                }
            }
            else
            {
                Console.WriteLine("Syntax error: the call must contain at least one web server url as argument");
            }
            listener.Start();
            foreach (string s in args)
            {
                Console.WriteLine("Listening for connections on " + s);
            }

            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }
                Console.WriteLine($"Received request for {request.Url}");
                Console.WriteLine(documentContents);

                if (request.Url.ToString() == "http://localhost:8080/index.html")
                {
                    String HTTP_ROOT = "..\\..\\www\\pub";  //server is in bin/debug folder
                    //String HTTP_ROOT = "C:\\Users\\louis\\Desktop\\Cours\\S8\\SOC\\TP1\\code\\TP1\\HttpListener\\BasicServerHttpListener\\www\\pub";    //Absolute path
                    HttpListenerResponse response = context.Response;

                    // Either remove this line from your current code or set it to "text/html"
                    response.ContentType = "text/html";

                    //read the file into byte array "buffer"
                    string path = HTTP_ROOT + context.Request.Url.AbsolutePath.Replace('/', '\\');
                    //string path2 = context.Request.Url.AbsolutePath.Replace('/', '\\');   //DEBUG
                    //Console.WriteLine($"Path : {path2}");
                    byte[] buffer;
                    try
                    {
                        FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                        FileInfo info = new FileInfo(path);
                        buffer = new byte[info.Length];
                        fs.Read(buffer, 0, Convert.ToInt32(info.Length));
                        fs.Close();
                    }
                    catch
                    {
                        return;
                    }

                    //send data back
                    response.Headers.Add("HttpResponseStatus:OK");
                    response.ContentLength64 = buffer.Length;
                    response.ContentType = "HTML";
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                    response.OutputStream.Flush();
                    response.Close();
                }
                else {  //If user gives bad path we display the default text

                    // Obtain a response object.
                    HttpListenerResponse response = context.Response;

                    // Construct a response.
                    string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = buffer.Length;
                    System.IO.Stream output = response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    // You must close the output stream.
                    output.Close();
                }

            }
            // Httplistener neither stop ...
            // listener.Stop();
        }
    }
}