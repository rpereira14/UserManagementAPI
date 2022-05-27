namespace UserManagementApi.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty ;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{this.FirstName} | {this.Surname} | {this.Email}";
        }
        public override bool Equals(object obj)
        {
            var item = obj as Customer;

            if (item == null)
            {
                return false;
            }

            return this.ToString().Equals(item.ToString());
        }
    }
}
