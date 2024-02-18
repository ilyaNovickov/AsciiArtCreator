using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiArtCreator.SystemDrawing.Framework
{
    public class GrayscaleArtFormatCollection : IEnumerable<GrayscaleArtFormat>
    {
        public class AsciiFormatEnumenator : IEnumerator<GrayscaleArtFormat>
        {
            private readonly IEnumerable<GrayscaleArtFormat> collection;
            private int index = -1;

            public AsciiFormatEnumenator(IEnumerable<GrayscaleArtFormat> collection)
            {
                this.collection = collection;
            }

            public object Current
            {
                get => collection.ElementAt(index);
            }

            GrayscaleArtFormat IEnumerator<GrayscaleArtFormat>.Current
            {
                get => collection.ElementAt(index);
            }

            public void Reset()
            {
                index = -1;
            }

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                if (index < collection.Count() - 1)
                {
                    index++;
                    return true;
                }
                return false;
            }
        }

        private List<GrayscaleArtFormat> asciiFormats = new List<GrayscaleArtFormat>();

        public GrayscaleArtFormatCollection()
        {

        }

        public GrayscaleArtFormatCollection(params GrayscaleArtFormat[] formats)
        {
            asciiFormats.AddRange(formats);
        }

        public GrayscaleArtFormatCollection(int capacity)
        {
            asciiFormats.Capacity = capacity;
        }

        public GrayscaleArtFormat this[string name]
        {
            get
            {
                return asciiFormats[Indexof(name)];
            }
            set
            {
                asciiFormats[Indexof(name)] = value;
            }
        }

        public bool Contains(string name)
        {
            IEnumerable<string> names = from format in asciiFormats select format.Name;
            return names.Contains(name);
        }
        public bool Contains(GrayscaleArtFormat asciiFormat)
        {
            return Contains(asciiFormat.Name);
        }
        public void Add(GrayscaleArtFormat asciiFormat)
        {
            if (Contains(asciiFormat))
                throw new Exception("Such ASCII format already exists");
            asciiFormats.Add(asciiFormat);
        }
        public bool TryToAdd(GrayscaleArtFormat asciiFormat)
        {
            if (Contains(asciiFormat))
                return false;
            asciiFormats.Add(asciiFormat);
            return true;
        }
        public bool Remove(GrayscaleArtFormat asciiFormat)
        {
            return asciiFormats.Remove(asciiFormat);
        }
        public int Indexof(GrayscaleArtFormat format)
        {
            return asciiFormats.IndexOf(format);
        }
        public int Indexof(string name)
        {
            List<string> names = (from format in asciiFormats select format.Name).ToList();
            return names.IndexOf(name);
        }
        public void Clear()
        {
            asciiFormats.Clear();
        }
        public List<GrayscaleArtFormat> ToList()
        {
            return new List<GrayscaleArtFormat>(asciiFormats);
        }

        public IEnumerator<GrayscaleArtFormat> GetEnumerator()
        {
            return new AsciiFormatEnumenator(asciiFormats);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new AsciiFormatEnumenator(asciiFormats);
        }
    }
}
