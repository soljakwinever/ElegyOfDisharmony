/***********************************************************\
 *  Formatted File
 *  By Aaron Stewart
 *  May 2013
 *  Designed to make writing of JSON Style format files easier
 * *********************************************************/
using System.IO;

namespace Utilities
{
    public class FormattedFile : System.IDisposable
    {
        #region Fields

        StreamWriter _baseWriter;
        StreamReader _baseReader;

        private bool
            _inblock,
            _writing,
            _reading;

        private int _tabLevel;

        private string _currentLine;

        #endregion

        #region Properties

        public string CurrentLine
        {
            get { return _currentLine; }
            set { _currentLine = value; }
        }

        #endregion

        #region Constructers

        public FormattedFile()
        {

        }
        ~FormattedFile()
        {
            Dispose();
        }

        #endregion

        #region Interface Methods

        public void Dispose()
        {
            if (_baseWriter != null)
                _baseWriter.Close();
            if (_baseReader != null)
                _baseReader.Close();
        }

        #endregion

        #region Read Methods

        /// <summary>
        /// Begin reading a file
        /// </summary>
        /// <param name="file">The file you want to open</param>
        public void ReadBegin(string file)
        {
            if (_writing)
                throw new System.Exception("Tried read while writing");
            if (!File.Exists(file))
                throw new System.Exception("No file exists");
            _baseReader = new StreamReader(File.OpenRead(file));
            _reading = true;
        }

        /// <summary>
        /// Stop reading the file
        /// </summary>
        public void ReadEnd()
        {
            if (_inblock)
                throw new System.Exception("Missing \'{\' in file");
            if (!_reading)
                throw new System.Exception("No file being read");
            _baseReader.Close();
            _reading = false;
        }

        /// <summary>
        /// Read in a block, gets the header name, and checks if there is a '{' where it should be
        /// </summary>
        /// <returns></returns>
        public string ReadBlock()
        {
            if (!_reading)
                throw new System.Exception("No file being read");
            _currentLine = RemoveTabRead();
            if (_currentLine == "{")
            {
                _inblock = _currentLine == "{";
            }
            else
                _inblock = RemoveTabRead() == "{";
            if (!_inblock)
                throw new System.Exception("Missing \'{\' in header " + _currentLine);
            return _currentLine;
        }

        /// <summary>
        /// Read in a line from the file
        /// </summary>
        /// <returns></returns>
        public string ReadLine()
        {
            if (!_reading)
                throw new System.Exception("No file being read");
            return RemoveTabRead();
        }

        /// <summary>
        /// Read the then of a block, always a '}' character
        /// </summary>
        /// <returns>Whether or not the end of the block is where it is expected</returns>
        public bool ReadEndBlock()
        {
            if (!_reading)
                throw new System.Exception("No file being read");
            _currentLine = RemoveTabRead();
            _inblock = !(_currentLine == "}");
            if (_inblock)
                throw new System.Exception("Missing \'{\' in header " + _currentLine);
            return !_inblock;
        }

        /// <summary>
        /// Used to remove the tab from read in files
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string RemoveTabRead()
        {
            return _baseReader.ReadLine().Split(new[] { '\t' }, System.StringSplitOptions.RemoveEmptyEntries)[0];
        }

        #endregion

        #region Write Methods

        /// <summary>
        /// Begins writing to a file, if it doesn't exist, it creates it
        /// </summary>
        /// <param name="file"></param>
        public void WriteBegin(string file)
        {
            if (_reading)
                throw new System.Exception("Tried writing while Reading");
            if (!File.Exists(file))
                using (var fs = File.Create(file))
                {
                    fs.Close();
                }
            _baseWriter = new StreamWriter(file,false);
            _writing = true;
        }

        /// <summary>
        /// Stop writing to the file
        /// </summary>
        public void WriteEnd()
        {
            if (!_writing)
                throw new System.Exception("Cannot close an unopened file");
            if(_tabLevel > 0)
                throw new System.Exception("Missing '}' in file");
            if (_tabLevel < 0)
                throw new System.Exception("Extra '}' in file");
            _baseWriter.Close();
            _writing = false;
        }

        /// <summary>
        /// Write a block to the file, with a header and a '{'
        /// </summary>
        /// <param name="headerName">The header to write</param>
        public void WriteBlock(string headerName)
        {
            if (!_writing)
                throw new System.Exception("Unable to write");
            string text = "", tabs = "";
            for (int i = 0; i < _tabLevel; i++)
                tabs += "\t";
            text += (headerName == "" ? tabs + "{" : tabs + headerName + "\n" + tabs + "{");
            _baseWriter.WriteLine(text);
            _tabLevel++;
        }

        /// <summary>
        /// Write a line to the file
        /// </summary>
        /// <param name="textToWrite"></param>
        public void WriteLine(object textToWrite)
        {
            string text = "",tabs = "";
            for (int i = 0; i < _tabLevel; i++)
                tabs += "\t";
            var lines = textToWrite.ToString().Split('\n');
            if(lines.Length > 1)
                for (int i = 0; i < lines.Length; i++)
                {
                    text += tabs + lines[i] + (i < lines.Length-1 ? "\n" : "");
                }
            else
                text += tabs + textToWrite;
            _baseWriter.WriteLine(text);
        }

        /// <summary>
        /// End the current block
        /// </summary>
        public void WriteEndBlock()
        {
            if (!_writing)
                throw new System.Exception("Unable to write");
            string text = "";
            _tabLevel--;
            for (int i = 0; i < _tabLevel; i++)
                text += "\t";
            text += "}";
            _baseWriter.WriteLine(text);
        }

        #endregion
    }
}
