namespace BasicsOfWebApi.Entities.User
{
    public class UsersDataStore
    {
        public static List<UserModel> Users = new List<UserModel>
        {
             new UserModel() { UserName = "Ahmad_Saleem", Email = "ahmad@saleem.com",FirstName = "Ahmad", LastName = "Saleem", Password = "easyP@ss",
             Role = "Admin"},
             new UserModel() { UserName = "khaled_mohammad", Email = "khaled@mohammad.com",FirstName = "khaled", LastName = "mohammad", Password = "easyP@ss123",
             Role = "User"},
             new UserModel() { UserName = "Omar_laith", Email = "omar@laith.com",FirstName = "Omar", LastName = "laith", Password = "P@$$w0rd",
             Role = "User"}
        };
    }
}
