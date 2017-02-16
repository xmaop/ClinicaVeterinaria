// Type: RazorJS.RazorJSFileParser
// Assembly: RazorJS, Version=0.4.3.0, Culture=neutral, PublicKeyToken=null
// MVID: B632E509-E703-4B0B-BC84-166B1A236F6F
// Assembly location: C:\Users\eflores\Desktop\Razor\RazorJS.dll

using PetCenter_GCP.RazorEngine;
using PetCenter_GCP.RazorEngine.Templating;
using PetCenter_GCP.RazorJS.Configuration;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;
using System.Web.Mvc;

namespace PetCenter_GCP.RazorJS
{
    public class RazorJSFileParser
    {
        private string _filename;

        public RazorJSFileParser(string filename)
        {
            //CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.AddRecord, 1, filename, null);
            //new LogCustomException().LogError(ExceptionEntity, "Error Razor", filename);
            this._filename = filename;
        }

        public string ScriptInclude(bool useTags = true)
        {
            return ((object)RazorJSFileParser.BuildScriptTag(string.Format("{0}?fn={1}", (object)GenericHelper.ResolveUrl(RazorJSSettings.Settings.HandlerPath), (object)GenericHelper.ResolveUrl(this._filename)))).ToString();
        }

        public string InlineScript<TModel>(TModel model, bool addScriptTags = true)
        {
            TagBuilder tagBuilder = RazorJSFileParser.BuildScriptTag("");
            string filePath = RazorJSFileParser.GetFilePath(this._filename);
            string str = this.ParseTemplateWithModel<TModel>(RazorJSFileParser.GetJs(filePath), model, filePath);
            if (!addScriptTags)
                return str;
            tagBuilder.InnerHtml = str;
            return ((object)tagBuilder).ToString();
        }

        public string InlineScript(bool addScriptTags = true)
        {
            TagBuilder tagBuilder = RazorJSFileParser.BuildScriptTag("");
            try
            {
                string filePath = RazorJSFileParser.GetFilePath(this._filename);
                string str = this.ParseTemplate(RazorJSFileParser.GetJs(filePath), filePath);
                if (!addScriptTags)
                    return str;
                tagBuilder.InnerHtml = str;
            }
            catch (Exception ex)
            {
                //CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.AddRecord, 1, ex.Message, ex);
                //new LogCustomException().LogError(ExceptionEntity, "Error Razor", ex.Source);
                throw ex;
            }
            return ((object)tagBuilder).ToString();
        }

        private static TagBuilder BuildScriptTag(string src = "")
        {
            TagBuilder tagBuilder = new TagBuilder("script");
            tagBuilder.Attributes["type"] = "text/javascript";
            if (!string.IsNullOrEmpty(src))
                tagBuilder.Attributes["src"] = src;
            return tagBuilder;
        }

        private static string GetJs(string filePath)
        {
            return CachedFileAccess.ReadAllText(filePath);
        }

        private string ParseTemplate(string template, string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(template))
                    return string.Empty;
                if (!CachedFileAccess.IsCompiled(name, new bool?()))
                {
                    Razor.SetTemplateBase(typeof(HtmlTemplateBase));
                    Razor.Compile(template, name);
                    CachedFileAccess.IsCompiled(name, new bool?(true));
                    return Razor.Run(name);
                }
                else
                    Razor.Compile(template, name);
                return Razor.Run(name);
            }
            catch (TemplateCompilationException ex)
            {
                //CustomDataValidationException ExceptionEntity = new CustomDataValidationException(Layer.Web, Module.AddRecord, 1, ex.Message, ex);
                //new LogCustomException().LogError(ExceptionEntity, "Error Razor", ex.Source);
                //throw ex;
                StringBuilder stringBuilder = new StringBuilder();
                foreach (CompilerError compilerError in ex.Errors)
                    stringBuilder.AppendFormat("{0}\n", (object)compilerError.ToString().Replace(compilerError.FileName, string.Empty));
                throw new JSFileParserException(string.Format("Failure to parse template {0}. See Errors:\n{1}", (object)this._filename, (object)((object)stringBuilder).ToString()));
            }
        }

        private string ParseTemplateWithModel<T>(string template, T model, string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(template))
                    return string.Empty;
                if (!CachedFileAccess.IsCompiled(name, new bool?()))
                {
                    Razor.SetTemplateBase(typeof(HtmlTemplateBase<>));
                    Razor.Compile(template, typeof(T), name);
                    CachedFileAccess.IsCompiled(name, new bool?(true));
                }
                return Razor.Run<T>(model, name);
            }
            catch (TemplateCompilationException ex)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (CompilerError compilerError in ex.Errors)
                    stringBuilder.AppendFormat("{0}\n", (object)compilerError.ToString().Replace(compilerError.FileName, string.Empty));
                throw new JSFileParserException(string.Format("Failure to parse template {0}. See Errors:\n{1}", (object)this._filename, (object)((object)stringBuilder).ToString()));
            }
        }

        private static string GetFilePath(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return string.Empty;
            string filename1 = HostingEnvironment.MapPath(filename);
            if (!RazorJSFileParser.IsValidFilename(filename1))
                throw new JSFileParserException(string.Format("File '{0}' is invalid or was not found. Only files with .js extension are valid.", (object)filename));
            else
                return filename1;
        }

        private static bool IsValidFilename(string filename)
        {
            if ((!Enumerable.Any<AllowedPathElement>(RazorJSSettings.Settings.AllowedPaths) || Enumerable.Any<AllowedPathElement>(RazorJSSettings.Settings.AllowedPaths, (Func<AllowedPathElement, bool>)(config => filename.StartsWith(HostingEnvironment.MapPath(config.Path))))) && Path.GetExtension(filename) == ".js")
                return File.Exists(filename);
            else
                return false;
        }
    }
}
