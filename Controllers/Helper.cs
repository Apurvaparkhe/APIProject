
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Infoapi.Models;
using System.Text;



namespace Infoapi.Services
{


    public class Helper
    {
        private readonly IConfiguration _config;

        public Helper(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<Value>> GetValuesAsync()
        {
            var values = new List<Value>();

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT Id, Name, Price,Quantity FROM valuefirst", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        values.Add(new Value
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Price = (string)reader["Price"],
                            Quantity = (int)reader["Quantity"]

                        });
                    }
                }
            }

            return values;
        }

        //To get product count 
        /*
        public async Task<int> GetProductCount()
        {
            int count;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT COUNT(*) FROM valuefirst", connection))
                {
                    count = (int)await command.ExecuteScalarAsync();
                }
            }
            Console.WriteLine(count);
            return count;
        }*/






        public bool AddValue(Value value)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                Console.WriteLine("Received Id: " + value.Id);
                Console.WriteLine("Received Name: " + value.Name);
                Console.WriteLine("Received price: " + value.Price);
                Console.WriteLine("Received quantity: " + value.Quantity);


                connection.Open();
                Console.WriteLine("connection is open ");

                using (var command = new SqlCommand("INSERT INTO valuefirst (Id, Name,Price,Quantity) VALUES (@Id, @Name,@Price,@Quantity)", connection))
                {
                    command.Parameters.AddWithValue("@Id", value.Id);
                    command.Parameters.AddWithValue("@Name", value.Name);
                    command.Parameters.AddWithValue("@Price", value.Price);
                    command.Parameters.AddWithValue("@Quantity", value.Quantity);



                    int affectedRows = command.ExecuteNonQuery();

                    return affectedRows > 0;

                }
            }
        }


        public bool UpdateValue(Value value)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Console.WriteLine("put connection is open ");
                Console.WriteLine("Received Id: " + value.Id);
                //Console.WriteLine("Received Name: " + value.Name);
                Console.WriteLine("Received Price: " + value.Price);


                using (var checkId = new SqlCommand("select count(*) from valuefirst where Id = @Id", connection))
                {
                    checkId.Parameters.AddWithValue("@Id", value.Id);
                    int result = (int)checkId.ExecuteScalar();
                    if (result == 0)
                    {
                        Console.WriteLine("Id is not present");
                        //return BadRequest();
                        return false;

                    }

                    else
                    {
                        using (var command = new SqlCommand("UPDATE valuefirst SET Price = @Price,Name=@Name, Quantity=@Quantity WHERE Id = @Id", connection))
                        {
                            //command.Parameters.AddWithValue("@Name", value.Name);
                            command.Parameters.AddWithValue("@Id", value.Id);
                            command.Parameters.AddWithValue("@Price", value.Price);
                            command.Parameters.AddWithValue("@Name", value.Name);
                            command.Parameters.AddWithValue("@Quantity", value.Quantity);


                            //command.ExecuteNonQuery();
                            int affectedRows = command.ExecuteNonQuery();

                            return affectedRows > 0;
                        }
                    }
                }
            }

        }

        public bool DeleteValue(int id)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Console.WriteLine("Delete connection is open ");

                using (var checkId = new SqlCommand("SELECT COUNT(*) FROM valuefirst WHERE Id = @Id", connection))
                {
                    checkId.Parameters.AddWithValue("@Id", id);
                    int result = (int)checkId.ExecuteScalar();
                    if (result == 0)
                    {
                        Console.WriteLine("Id is not present");
                        return false;
                    }

                    else
                    {
                        using (var command = new SqlCommand("DELETE from valuefirst WHERE Id = @Id", connection))

                        {
                            command.Parameters.AddWithValue("@Id", id);
                            //command.ExecuteNonQuery();
                            int affectedRows = command.ExecuteNonQuery();

                            return affectedRows > 0;

                        }
                    }
                }
            }

            // return NoContent();
            //return true;

        }

        public bool GetbyId(int id)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Console.WriteLine("Get by id  connection is open ");
                Console.WriteLine("Received id is " + id);


                using (var checkId = new SqlCommand("select count(*) from valuefirst where Id = @Id", connection))
                {
                    checkId.Parameters.AddWithValue("@Id", id);
                    int result = (int)checkId.ExecuteScalar();
                    if (result == 0)
                    {
                        Console.WriteLine("Id is not present");
                        //return BadRequest();
                        return false;

                    }
                    else
                    {

                        Console.WriteLine("Id is present");
                        //return BadRequest();
                        return true;

                    }
                }

            }


        }




        // User side code 

        //check login credential
        /*
        public bool CheckCred(Cred cred)
        {

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Console.WriteLine("Credential check connection is open ");
                //Console.WriteLine("Received id  "+cred.Id);

                Console.WriteLine("Received email is " + cred.Email);
                Console.WriteLine("Received password is " + cred.Password);



                using (var checkId = new SqlCommand("select count(*) from Credential where Email = @Email AND Password=@Password ", connection))
                {
                    // checkId.Parameters.AddWithValue("@Id", cred.Id);

                    checkId.Parameters.AddWithValue("@Email", cred.Email);
                    checkId.Parameters.AddWithValue("@Password", cred.Password);

                    int result = (int)checkId.ExecuteScalar();
                    if (result == 0)
                    {
                        Console.WriteLine("Wrong Credentials");
                        //return BadRequest();
                        return false;

                    }
                    else
                    {

                        Console.WriteLine("Correct Credential");
                        //return BadRequest();
                        return true;

                    }
                }

            }


        }*/

        /*
                //To check the credentials
                public string CheckCred(Cred cred)
                {
                    using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        Console.WriteLine("Credential check connection is open ");
                        Console.WriteLine("Received email is " + cred.Email);
                        Console.WriteLine("Received password is " + cred.Password);
                         // Check if Rolename is null
                         string rolename = cred.Rolename ?? "";

                       // using (var checkId = new SqlCommand("select Password from Users where Email = @Email", connection))
                        using (var checkId = new SqlCommand("select Password, Rolename from Users inner join Roles on Users.RoleId = Roles.RoleId where Email = @Email", connection))

                        {
                            checkId.Parameters.AddWithValue("@Email", cred.Email);

                            string encryptedPassword = (string)checkId.ExecuteScalar();

                            if (encryptedPassword == null)
                            {
                                Console.WriteLine("Wrong Credentials");
                                return null;
                            }
                            Console.WriteLine("Encrypt one" + encryptedPassword);
                            string decryptedPassword = new MyNamespace.PasswordEncryptor().DecryptPassword(encryptedPassword);
                            Console.WriteLine("Decrypt one" + decryptedPassword);
                            if (cred.Password == decryptedPassword)
                            {
                                Console.WriteLine("Correct Credential");
                                //return true;
                                return rolename;
                            }
                            else
                            {
                                Console.WriteLine("Wrong Credentials");
                                return null;
                            }
                        }
                    }
                }

                */
        //To check the credentials
        public string? CheckCred(Cred cred)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Console.WriteLine("Credential check connection is open ");
                Console.WriteLine("Received email is " + cred.Email);
                Console.WriteLine("Received password is " + cred.Password);

                using (var checkId = new SqlCommand("select Password, Rolename from Users inner join Roles on Users.RoleId = Roles.RoleId where Email = @Email", connection))
                {
                    checkId.Parameters.AddWithValue("@Email", cred.Email);

                    SqlDataReader reader = checkId.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("Wrong Credentials");
                        return null;
                    }

                    reader.Read();
                    string encryptedPassword = reader.GetString(0);
                    string rolename = reader.GetString(1);

                    Console.WriteLine("Encrypt one" + encryptedPassword);
                    string decryptedPassword = new MyNamespace.PasswordEncryptor().DecryptPassword(encryptedPassword);
                    Console.WriteLine("Decrypt one" + decryptedPassword);

                    if (cred.Password == decryptedPassword)
                    {
                        Console.WriteLine("Correct Credential");
                        Console.WriteLine(rolename);
                        return rolename;
                    }
                    else
                    {
                        Console.WriteLine("Wrong Credentials");
                        return null;
                    }
                }
            }
        }





        // Get all user data 
        public async Task<IEnumerable<Cred>> GetCred()
        {
            var creds = new List<Cred>();

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT Users.Email, Users.Password, Roles.Rolename FROM Users INNER JOIN Roles ON Users.RoleId = Roles.RoleId;", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        creds.Add(new Cred
                        {
                            //Id = (int)reader["Id"],
                            // Id = (int)reader["RoleId"],
                            Rolename = (string)reader["Rolename"],
                            Email = (string)reader["Email"],
                            // Password = (string)reader["Password"]
                            Password = new MyNamespace.PasswordEncryptor().DecryptPassword((string)reader["Password"])

                        });
                    }
                }
            }

            return creds;
        }

        public async Task<int?> GetUserCount()
        {
            int? count = null;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT COUNT(*) FROM Users", connection))
                {
                    var result = await command.ExecuteScalarAsync();
                    if (result != null)
                    {
                        count = (int)result;
                    }
                }
            }
            Console.WriteLine(count);
            return count;
        }




        //Get user data by Email

        public bool GetUserbyId(string email)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Console.WriteLine("Get by email  connection is open ");
                Console.WriteLine("Received email is " + email);


                using (var checkId = new SqlCommand("select count(*) from Users where Email = @Email", connection))
                {
                    checkId.Parameters.AddWithValue("@Email", email);
                    int result = (int)checkId.ExecuteScalar();
                    if (result == 0)
                    {
                        Console.WriteLine("Email is not present");
                        //return BadRequest();
                        return false;

                    }
                    else
                    {

                        Console.WriteLine("Email is present");
                        //return BadRequest();
                        return true;

                    }
                }

            }


        }


        //Add New user (Post)

        public bool AddNewUser(Cred cred)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                // Console.WriteLine("Received Id: " + cred.Id);
                Console.WriteLine("Received Email: " + cred.Email);
                Console.WriteLine("Received password: " + cred.Password);
                Console.WriteLine("Received Rolename: " + cred.Rolename);

                if (cred.Password != null)
                {
                    var encryptor = new MyNamespace.PasswordEncryptor();
                    var encryptedPassword = encryptor.EncryptPassword(cred.Password);
                    Console.WriteLine(encryptedPassword);

                    connection.Open();
                    Console.WriteLine("connection is open ");

                    int roleId;
                    using (var command = new SqlCommand("SELECT RoleId FROM Roles WHERE Rolename = @Rolename", connection))
                    {
                        command.Parameters.AddWithValue("@Rolename", cred.Rolename);
                        roleId = (int)command.ExecuteScalar();
                    }

                    using (var command = new SqlCommand("INSERT INTO Users (RoleId, Email, Password) VALUES (@RoleId, @Email, @Password)", connection))
                    {
                        command.Parameters.AddWithValue("@RoleId", roleId);
                        command.Parameters.AddWithValue("@Email", cred.Email);
                        command.Parameters.AddWithValue("@Password", encryptedPassword);

                        int affectedRows = command.ExecuteNonQuery();
                        return affectedRows > 0;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        // EDIT user 
        public bool UpdateUser(Cred cred)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Console.WriteLine("put connection is open ");
                //Console.WriteLine("Received Id: " + cred.Id);
                Console.WriteLine("Received Name: " + cred.Email);
                Console.WriteLine("Received Password: " + cred.Password);

                if (cred.Password != null)
                {
                    var encryptor = new MyNamespace.PasswordEncryptor();
                    var encryptedPassword = encryptor.EncryptPassword(cred.Password);
                    Console.WriteLine(encryptedPassword);



                    using (var checkId = new SqlCommand("select count(*) from Users where Email = @Email", connection))
                    {
                        checkId.Parameters.AddWithValue("@Email", cred.Email);
                        int result = (int)checkId.ExecuteScalar();
                        if (result == 0)
                        {
                            Console.WriteLine("Email is not present");
                            //return BadRequest();
                            return false;

                        }

                        else
                        {
                            using (var command = new SqlCommand("UPDATE Users SET Password=@Password WHERE Email = @Email", connection))
                            {
                                // command.Parameters.AddWithValue("@Id", cred.Id);
                                command.Parameters.AddWithValue("@Email", cred.Email);
                                command.Parameters.AddWithValue("@Password", encryptedPassword);


                                //command.ExecuteNonQuery();
                                int affectedRows = command.ExecuteNonQuery();

                                return affectedRows > 0;
                            }
                        }
                    }

                }
                else
                {
                    return false;
                }
            }

        }




        // EDIT user credential from home page 
        public bool UpdateUserCred(Cred cred)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Console.WriteLine("put connection is open ");
                //Console.WriteLine("Received Id: " + cred.Id);
                Console.WriteLine("Received Name: " + cred.Email);
                Console.WriteLine("Received Password: " + cred.Password);


                if (cred.Password != null)
                {
                    var encryptor = new MyNamespace.PasswordEncryptor();
                    var encryptedPassword = encryptor.EncryptPassword(cred.Password);
                    Console.WriteLine(encryptedPassword);



                    using (var checkId = new SqlCommand("select count(*) from Users where Email = @Email", connection))
                    {
                        checkId.Parameters.AddWithValue("@Email", cred.Email);
                        int result = (int)checkId.ExecuteScalar();
                        if (result == 0)
                        {
                            Console.WriteLine("Email is not present");
                            //return BadRequest();
                            return false;

                        }

                        else
                        {
                            using (var command = new SqlCommand("UPDATE Users SET Password=@Password WHERE Email = @Email", connection))
                            {
                                // command.Parameters.AddWithValue("@Id", cred.Id);
                                command.Parameters.AddWithValue("@Email", cred.Email);
                                command.Parameters.AddWithValue("@Password", encryptedPassword);


                                //command.ExecuteNonQuery();
                                int affectedRows = command.ExecuteNonQuery();

                                return affectedRows > 0;
                            }
                        }
                    }

                }
                else
                {
                    return false;
                }
            }

        }




        // Delete User 

        public bool DeleteUsers(string email)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Console.WriteLine("Delete connection is open ");

                using (var checkId = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Email = @Email", connection))
                {
                    checkId.Parameters.AddWithValue("@Email", email);
                    int result = (int)checkId.ExecuteScalar();
                    if (result == 0)
                    {
                        Console.WriteLine("Email is not present");
                        return false;
                    }

                    else
                    {
                        using (var command = new SqlCommand("DELETE from Users WHERE Email = @Email", connection))

                        {
                            command.Parameters.AddWithValue("@Email", email);
                            //command.ExecuteNonQuery();
                            int affectedRows = command.ExecuteNonQuery();

                            return affectedRows > 0;

                        }
                    }
                }
            }

            // return NoContent();
            // return true;

        }






        // Get all selenium data 
        public async Task<IEnumerable<Selenium>> GetSelenium()
        {
            var sel = new List<Selenium>();

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM employee2", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        sel.Add(new Selenium
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Mobile_No = (string)reader["Mobile_No"],
                            Email = (string)reader["Email"],
                            Address = (string)reader["Address"]


                        });
                    }
                }
            }

            return sel;
        }


        //To get selenium count 

        /*public async Task<int?> GetSeleniumCount()
        {
            int? count;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT COUNT(*) FROM employee2", connection))
                {
                    var result = await command.ExecuteScalarAsync();
                    if (result != null)
                    {
                        count = (int)result;
                    }
                }

            }
            Console.WriteLine(count);
            return count;
        }*/

        public async Task<int?> GetSeleniumCount()
        {
            int? count = null;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT COUNT(*) FROM employee2", connection))
                {
                    var result = await command.ExecuteScalarAsync();
                    if (result != null)
                    {
                        count = (int)result;
                    }
                }
            }
            Console.WriteLine(count);
            return count;
        }






        //Get selenium data by ID

        public bool GetSeleniumbyId(int id)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Console.WriteLine("Get by id  connection is open ");
                Console.WriteLine("Received id is " + id);


                using (var checkId = new SqlCommand("select count(*) from employee2 where Id = @Id", connection))
                {
                    checkId.Parameters.AddWithValue("@Id", id);
                    int result = (int)checkId.ExecuteScalar();
                    if (result == 0)
                    {
                        Console.WriteLine("Id is not present");
                        //return BadRequest();
                        return false;

                    }
                    else
                    {

                        Console.WriteLine("Id is present");
                        //return BadRequest();
                        return true;

                    }
                }

            }


        }


        //Add selenium data (Post)

        public bool AddNewSeleniumData(Selenium sel)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                //Console.WriteLine("Received Id: " + sel.Id);
                Console.WriteLine("Received Email: " + sel.Name);
                Console.WriteLine("Received password: " + sel.Mobile_No);
                Console.WriteLine("Received password: " + sel.Email);
                Console.WriteLine("Received password: " + sel.Address);



                connection.Open();
                Console.WriteLine("connection is open ");

                using (var command = new SqlCommand("INSERT INTO employee2 (Name,Mobile_No, Email,Address) VALUES (@Name,@Mobile_No, @Email,@Address)", connection))
                {
                    //command.Parameters.AddWithValue("@Id", sel.Id);
                    command.Parameters.AddWithValue("@Name", sel.Name);
                    command.Parameters.AddWithValue("@Mobile_No", sel.Mobile_No);

                    command.Parameters.AddWithValue("@Email", sel.Email);
                    command.Parameters.AddWithValue("@Address", sel.Address);

                    int affectedRows = command.ExecuteNonQuery();

                    return affectedRows > 0;

                }
            }
        }



        // Delete Selenium data 

        public bool DeleteSelenium(int id)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Console.WriteLine("Delete connection is open ");

                using (var checkId = new SqlCommand("SELECT COUNT(*) FROM employee2 WHERE Id = @Id", connection))
                {
                    checkId.Parameters.AddWithValue("@Id", id);
                    int result = (int)checkId.ExecuteScalar();
                    if (result == 0)
                    {
                        Console.WriteLine("Id is not present");
                        return false;
                    }

                    else
                    {
                        using (var command = new SqlCommand("DELETE from employee2 WHERE Id = @Id", connection))

                        {
                            command.Parameters.AddWithValue("@Id", id);
                            //command.ExecuteNonQuery();
                            int affectedRows = command.ExecuteNonQuery();

                            return affectedRows > 0;

                        }
                    }
                }
            }

            // return NoContent();
           // return true;

        }



        // EDIT Selenium data 
        public bool UpdateSelenium(Selenium sel)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Console.WriteLine("put connection is open ");
                Console.WriteLine("Received Id: " + sel.Id);
                Console.WriteLine("Received Name: " + sel.Name);
                Console.WriteLine("Received Name: " + sel.Mobile_No);

                Console.WriteLine("Received Name: " + sel.Email);
                Console.WriteLine("Received Price: " + sel.Address);


                using (var checkId = new SqlCommand("select count(*) from employee2 where Id = @Id", connection))
                {
                    checkId.Parameters.AddWithValue("@Id", sel.Id);
                    int result = (int)checkId.ExecuteScalar();
                    if (result == 0)
                    {
                        Console.WriteLine("Id is not present");
                        //return BadRequest();
                        return false;

                    }

                    else
                    {
                        using (var command = new SqlCommand("UPDATE employee2 SET Mobile_No=@Mobile_No, Email = @Email,Address=@Address WHERE Id = @Id", connection))
                        {
                            command.Parameters.AddWithValue("@Id", sel.Id);
                            command.Parameters.AddWithValue("@Mobile_No", sel.Mobile_No);

                            command.Parameters.AddWithValue("@Email", sel.Email);
                            command.Parameters.AddWithValue("@Address", sel.Address);


                            //command.ExecuteNonQuery();
                            int affectedRows = command.ExecuteNonQuery();

                            return affectedRows > 0;
                        }
                    }
                }
            }

        }



        //Get patent data by ID 

        public bool GetPatentbyId(int id)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Console.WriteLine("Get by id  connection is open ");
                Console.WriteLine("Received id is " + id);


                using (var checkId = new SqlCommand("select count(*) from Univercity where Id = @Id", connection))
                {
                    checkId.Parameters.AddWithValue("@Id", id);
                    int result = (int)checkId.ExecuteScalar();
                    if (result == 0)
                    {
                        Console.WriteLine("Id is not present");
                        //return BadRequest();
                        return false;

                    }
                    else
                    {

                        Console.WriteLine("Id is present");
                        //return BadRequest();
                        return true;

                    }
                }

            }


        }


        // Get all Patent data 
        public async Task<IEnumerable<Patent>> GetPatent()
        {
            var pet = new List<Patent>();

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Univercity", connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        pet.Add(new Patent
                        {
                            Id = (int)reader["Id"],
                            Application = (string)reader["Application"],

                            Registration = (string)reader["Registration"],
                            Filing_date = (string)reader["Filing_date"],
                            Status = (string)reader["Status"],
                            Status_date = (string)reader["Status_date"],
                            AppType = (string)reader["AppType"],
                            Date_Rec = (string)reader["Date_Rec"],
                            PriorityInfo = (string)reader["PriorityInfo"],
                            Holder = (string)reader["Holder"],
                            RegDate = (string)reader["RegDate"],
                            NoOfPatent = (string)reader["NoOfPatent"],
                            Representative = (string)reader["Representative"],
                            PublicationDate = (string)reader["PublicationDate"],
                            Inventor = (string)reader["Inventor"],
                            Examiner = (string)reader["Examiner"]



                        });
                    }
                }
            }

            return pet;
        }


        //To get Patent count 

       /*for now not in  use because i have one method to get all tables count
        public async Task<int> GetpatentCount()
        {
           int count;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT COUNT(*) FROM Univercity", connection))
                {
                    count = (int)await command.ExecuteScalarAsync();
                }
            }
            Console.WriteLine(count);
            
            return count;
            
        }*/


        //Add patent data (Post)

        public bool AddNewPatentData(Patent pet)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                //Console.WriteLine("Received Id: " + pet.Id);
                Console.WriteLine("Received Email: " + pet.Application);
                Console.WriteLine("Received password: " + pet.Registration);
                Console.WriteLine("Received password: " + pet.AppType);
                Console.WriteLine("Received password: " + pet.Date_Rec);
                Console.WriteLine("Received password: " + pet.Examiner);
                Console.WriteLine("Received password: " + pet.Filing_date);
                Console.WriteLine("Received password: " + pet.Holder);
                Console.WriteLine("Received password: " + pet.Inventor);
                Console.WriteLine("Received password: " + pet.NoOfPatent);
                Console.WriteLine("Received password: " + pet.PublicationDate);
                Console.WriteLine("Received password: " + pet.Representative);




                connection.Open();
                Console.WriteLine("connection is open ");

                using (var Command = new SqlCommand("INSERT INTO Univercity (Application,Registration,Filing_date,Status,Status_date,AppType ,Date_Rec,PriorityInfo,Holder ,RegDate ,NoOfPatent ,Representative , PublicationDate ,Inventor ,Examiner) VALUES (@v1,@v2,@v3,@v4,@v5,@v6,@v7,@v8,@v9,@v10,@v11,@v12,@v13,@v14,@v15)", connection))
                {
                    // Command.Parameters.AddWithValue("@Id", pet.Id);
                    Command.Parameters.AddWithValue("@v1", pet.Application);
                    Command.Parameters.AddWithValue("@v2", pet.Registration);
                    Command.Parameters.AddWithValue("@v3", pet.Filing_date);
                    Command.Parameters.AddWithValue("@v4", pet.Status);
                    Command.Parameters.AddWithValue("@v5", pet.Status_date);
                    Command.Parameters.AddWithValue("@v6", pet.AppType);
                    Command.Parameters.AddWithValue("@v7", pet.Date_Rec);
                    Command.Parameters.AddWithValue("@v8", pet.PriorityInfo);

                    Command.Parameters.AddWithValue("@v9", pet.Holder);
                    Command.Parameters.AddWithValue("@v10", pet.RegDate);
                    Command.Parameters.AddWithValue("@v11", pet.NoOfPatent);
                    Command.Parameters.AddWithValue("@v12", pet.Representative);
                    Command.Parameters.AddWithValue("@v13", pet.PublicationDate);
                    Command.Parameters.AddWithValue("@v14", pet.Inventor);
                    Command.Parameters.AddWithValue("@v15", pet.Examiner);


                    int affectedRows = Command.ExecuteNonQuery();

                    return affectedRows > 0;

                }
            }
        }


        // Delete Patent data 

        public bool DeletePatent(int id)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Console.WriteLine("Delete connection is open ");

                using (var checkId = new SqlCommand("SELECT COUNT(*) FROM Univercity WHERE Id = @Id", connection))
                {
                    checkId.Parameters.AddWithValue("@Id", id);
                    int result = (int)checkId.ExecuteScalar();
                    if (result == 0)
                    {
                        Console.WriteLine("Id is not present");
                        return false;
                    }

                    else
                    {
                        using (var command = new SqlCommand("DELETE from Univercity WHERE Id = @Id", connection))

                        {
                            command.Parameters.AddWithValue("@Id", id);
                            //command.ExecuteNonQuery();
                            int affectedRows = command.ExecuteNonQuery();

                            return affectedRows > 0;

                        }
                    }
                }
            }

            // return NoContent();
           // return true;

        }

        // EDIT Patent data 
        public bool UpdatePatent(Patent pet)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                Console.WriteLine("Received Email: " + pet.Application);
                Console.WriteLine("Received password: " + pet.Registration);
                Console.WriteLine("Received password: " + pet.AppType);
                Console.WriteLine("Received password: " + pet.Date_Rec);
                Console.WriteLine("Received password: " + pet.Examiner);
                Console.WriteLine("Received password: " + pet.Filing_date);
                Console.WriteLine("Received password: " + pet.Holder);
                Console.WriteLine("Received password: " + pet.Inventor);
                Console.WriteLine("Received password: " + pet.NoOfPatent);
                Console.WriteLine("Received password: " + pet.PublicationDate);
                Console.WriteLine("Received password: " + pet.Representative);

                using (var checkId = new SqlCommand("select count(*) from Univercity where Id = @Id", connection))
                {
                    checkId.Parameters.AddWithValue("@Id", pet.Id);
                    int result = (int)checkId.ExecuteScalar();
                    if (result == 0)
                    {
                        Console.WriteLine("Id is not present");
                        //return BadRequest();
                        return false;

                    }

                    else
                    {
                        using (var Command = new SqlCommand("UPDATE Univercity SET Filing_date=@v3,Status=@v4,Status_date=@v5,AppType=@v6 ,Date_Rec=@v7,PriorityInfo=@v8,Holder=@v9,RegDate=@v10 ,NoOfPatent=@v11 ,Representative=@v12,PublicationDate=@v13 ,Inventor=@v14 ,Examiner=@v15 WHERE Id = @v1", connection))
                        {
                            Command.Parameters.AddWithValue("@v1", pet.Id);

                            Command.Parameters.AddWithValue("@v3", pet.Filing_date);
                            Command.Parameters.AddWithValue("@v4", pet.Status);
                            Command.Parameters.AddWithValue("@v5", pet.Status_date);
                            Command.Parameters.AddWithValue("@v6", pet.AppType);
                            Command.Parameters.AddWithValue("@v7", pet.Date_Rec);
                            Command.Parameters.AddWithValue("@v8", pet.PriorityInfo);
                            Command.Parameters.AddWithValue("@v9", pet.Holder);
                            Command.Parameters.AddWithValue("@v10", pet.RegDate);
                            Command.Parameters.AddWithValue("@v11", pet.NoOfPatent);
                            Command.Parameters.AddWithValue("@v12", pet.Representative);
                            Command.Parameters.AddWithValue("@v13", pet.PublicationDate);
                            Command.Parameters.AddWithValue("@v14", pet.Inventor);
                            Command.Parameters.AddWithValue("@v15", pet.Examiner);

                            //command.ExecuteNonQuery();
                            int affectedRows = Command.ExecuteNonQuery();

                            return affectedRows > 0;
                        }
                    }
                }
            }

        }





    }

}