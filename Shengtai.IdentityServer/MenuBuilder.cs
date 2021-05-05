using Shengtai.IdentityServer.Models.Shared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public abstract class MenuBuilder : IList<INavHeader>
    {
        private readonly IList<INavHeader> _headers;

        protected MenuBuilder()
        {
            _headers = new List<INavHeader>();

            this.Initialize();
        }

        public INavHeader this[int index] { get => _headers[index]; set => _headers[index] = value; }

        public int Count => _headers.Count;

        public bool IsReadOnly => _headers.IsReadOnly;

        public void Add(INavHeader item)
        {
            _headers.Add(item);
        }

        public void Clear()
        {
            _headers.Clear();
        }

        public bool Contains(INavHeader item)
        {
            return _headers.Contains(item);
        }

        public void CopyTo(INavHeader[] array, int arrayIndex)
        {
            _headers.CopyTo(array, arrayIndex);
        }

        public IEnumerator<INavHeader> GetEnumerator()
        {
            return _headers.GetEnumerator();
        }

        public int IndexOf(INavHeader item)
        {
            return _headers.IndexOf(item);
        }

        public void Insert(int index, INavHeader item)
        {
            _headers.Insert(index, item);
        }

        public bool Remove(INavHeader item)
        {
            return _headers.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _headers.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException("The method or operation is not implemented.");
        }

        public abstract void Initialize();

        public abstract bool ShowStrategy(Menu sender, IList<string> roles);
    }
}
