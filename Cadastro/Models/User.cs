using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Cadastro.Models
{
   public class User
   {
      [Required]
      [Display(Name = "User name")]
      public string UserName { get; set; }

      [Required]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; }

      [Display(Name = "Remember on this computer")]
      public bool RememberMe { get; set; }

      private string Encode(string value)
      {
         var hash = System.Security.Cryptography.SHA1.Create();
         var encoder = new System.Text.ASCIIEncoding();
         var combined = encoder.GetBytes(value ?? "");
         return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
      }

      public bool IsValid(string _username, string _password)
      {
         string cs = ConfigurationManager.ConnectionStrings["GerenciamentoContextLogin"].ConnectionString;

         using (var cn = new SqlConnection(cs))
         {
            string sql = @"SELECT [Username] FROM [dbo].[System_Users] " + @"WHERE [Username] = @u AND [Password] = @p";

            var cmd = new SqlCommand(sql, cn);

            cmd.Parameters
                .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                .Value = _username;
            cmd.Parameters
                .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                .Value = Encode(_password);
            cn.Open();
            var reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
               reader.Dispose();
               cmd.Dispose();
               return true;
            }
            else
            {
               reader.Dispose();
               cmd.Dispose();
               return false;
            }
         }
      }
   }
}