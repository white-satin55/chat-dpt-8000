

using System.Collections;

namespace ChatServer
{
    public class ClientRepository : IEnumerable<Client>
    {
        private List<Client> _clients;
        public IReadOnlyList<Client> Clients
        {
            get { return _clients.AsReadOnly(); }
        }

        public void Add(Client client)
        {
            _clients.Add(client);
        }
       
        public void Remove(Client client)
        {
            _clients.Remove(client);
        }
        
        public IEnumerator<Client> GetEnumerator()
        {
            return Clients.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
