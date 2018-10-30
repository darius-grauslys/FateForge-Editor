using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FateForge.Utility
{
    public enum YamlWriterMode
    {
        INLINE,
        INDENT,
        SEQUENCE,
        MAPPING
    }

    public class YamlWriter
    {
        private string _indent;
        private YamlWriterMode _writingMode = YamlWriterMode.INLINE;
        private int _indentationLevel = 0;
        private TextWriter _textWriter;

        public int IndentationLevel { get => _indentationLevel; set => _indentationLevel = value; }
        private TextWriter TextWriter { get => _textWriter; set => _textWriter = value; }
        public YamlWriterMode WritingMode { get => _writingMode; set => _writingMode = value; }
        private string Indent { get => _indent; set => _indent = value; }

        public YamlWriter(TextWriter _textWriter)
        {
            TextWriter = _textWriter;
        }

        public void WriteFullScalar(string scalarName, string val)
        {
            if (WritingMode == YamlWriterMode.INLINE)
            {
                SetIndent();
                WriteLine(scalarName + ": " + val);
            }
            else
            {
                WriteScalarName(scalarName);
                WriteLine("  " + val);
            }
        }

        public void WriteScalarName(string scalarName)
        {
            SetIndent();
            WritingMode = YamlWriterMode.INDENT;
            WriteLine(scalarName + ":");
        }

        public void Close()
        {
            TextWriter.Close();
        }

        private void SetIndent()
        {
            string Indent = "";
            for (int i = _indentationLevel; i > 0; i--)
                Indent += "  ";
        }

        private void WriteLine(string write)
        {
            TextWriter.WriteLine(Indent + write);
        }
    }

}
