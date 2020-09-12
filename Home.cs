using System.IO;
using Nancy;
using System;
using System.Text;

namespace Xml2CSharp.Web
{
    public class Home : NancyModule
    {
        private object stringWriter;

        public Home()
        {
            Get["/"] = parameters =>
            {
                return View["Index"];
            };

            Post["/"] = parameters =>
            {
                var xml = this.Request.Form.xml;
                var classInfo = new Xml2CSharpConverer().Convert(xml);

                var classInfoWriter = new ClassInfoWriter(classInfo);

                var stringWriter = new StringWriter();
                classInfoWriter.Write(stringWriter);
                File.WriteAllText(@"E:\_MIS_Moalemi\final.cs", stringWriter.ToString());
                return View["result", new ConvertResponse { CSharpCode = stringWriter.ToString() }];
               

            };
            
        }

    }
    
    public class ConvertResponse
    {
        public string CSharpCode { get; set; }
    }
}