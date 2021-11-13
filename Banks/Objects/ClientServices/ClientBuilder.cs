using Banks.Tools;

namespace Banks.Objects.ClientServices
{
    public class ClientBuilder
    {
        private string _name;
        private string _surname;
        private string _address;
        private string _passport;

        public ClientBuilder AddName(string name)
        {
            _name = name ?? throw new CentralBankException("Incorrect name");
            return this;
        }

        public ClientBuilder AddSurname(string surname)
        {
            _surname = surname ?? throw new CentralBankException("Incorrect surname");
            return this;
        }

        public ClientBuilder AddAddress(string address)
        {
            _address = address ?? throw new CentralBankException("Incorrect address");
            return this;
        }

        public ClientBuilder AddPassport(string passport)
        {
            _passport = passport ?? throw new CentralBankException("Incorrect passport");
            return this;
        }

        public Client GetClient()
        {
            return new Client(_name, _surname, _address, _passport);
        }
    }
}