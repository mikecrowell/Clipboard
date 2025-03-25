using System;

namespace FieldTool.Entity
{
    public abstract class Contextual
    {
        public Contextual()
        {
        }

        public Contextual(String connectionString)
        {
            this._connectionString = connectionString;
        }

        public Contextual(IClipBoardUpload context)
        {
            this.Context = context;
        }

        public IClipBoardUpload Context
        {
            get
            {
                if (this._context != default(IClipBoardUpload) && this._context.GetType() == typeof(FakeClipBoardUpload))
                {
                    return this._context;
                }
                else
                {
                    return (String.IsNullOrEmpty(this._connectionString)) ? new ClipBoardUpload() : new ClipBoardUpload(this._connectionString);
                }
            }
            set { this._context = value; }
        }

        private string _connectionString = String.Empty;
        private IClipBoardUpload _context;
    }
}