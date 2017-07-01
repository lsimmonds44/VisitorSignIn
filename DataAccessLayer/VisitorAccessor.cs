using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class VisitorAccessor
    {
        public static int CreateVisitor(Visitor newVisitor)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            const string cmdText = @"sp_create_visitor";
            var cmd = new SqlCommand(cmdText, conn);
            
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", newVisitor.FirstName);
            cmd.Parameters.AddWithValue("@LastName", newVisitor.LastName);
            cmd.Parameters.AddWithValue("@Company", newVisitor.Company);
            cmd.Parameters.AddWithValue("@PersonVisiting", newVisitor.PersonVisiting);
            cmd.Parameters.AddWithValue("@Citizen", newVisitor.Citizen);
            cmd.Parameters.AddWithValue("@SignedIn", newVisitor.SignedIn);
            cmd.Parameters.AddWithValue("@SignedInTime", newVisitor.SignedInTime);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public static List<Visitor> RetrieveSignedInList(bool signedIn)
        {
            var signedInRecords = new List<Visitor>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_signed_in";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SignedIn", SqlDbType.Bit);
            cmd.Parameters["@SignedIn"].Value = signedIn;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        signedInRecords.Add(new Visitor()
                        {
                            FirstName = reader.GetString(0),
                            LastName = reader.GetString(1),
                            Company = reader.GetString(2),
                            PersonVisiting = reader.GetString(3),
                            Citizen = reader.GetBoolean(4),
                            SignedIn = reader.GetBoolean(5),
                            SignedInTime = reader.GetDateTime(6),
                            VisitorID = reader.GetInt32(7)
                        });
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return signedInRecords;
        }

        public static Visitor RetrieveVisitor(int visitorID)
        {
            var visitor = new Visitor();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_visitor_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@VisitorID", visitorID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        visitor = new Visitor()
                        {
                            FirstName = reader.GetString(0),
                            LastName = reader.GetString(1),
                            Company = reader.GetString(2),
                            PersonVisiting = reader.GetString(3),
                            Citizen = reader.GetBoolean(4),
                            SignedIn = reader.GetBoolean(5),
                            SignedInTime = reader.GetDateTime(6),
                            VisitorID = reader.GetInt32(7)
                        };
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return visitor;
        }

        public static int VisitorSignOut(int visitorID)
        {
            int rows = 0;

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_check_out_visitor";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@VisitorID", visitorID);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return rows;
        }

        public static List<Visitor> RetrieveSignedOutList(bool signedOut)
        {
            var signedOutRecords = new List<Visitor>();

            var conn = DBConnection.GetDBConnection();
            var cmdText = @"sp_retrieve_signed_out";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SignedIn", SqlDbType.Bit);
            cmd.Parameters["@SignedIn"].Value = signedOut;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        signedOutRecords.Add(new Visitor()
                        {
                            FirstName = reader.GetString(0),
                            LastName = reader.GetString(1),
                            Company = reader.GetString(2),
                            PersonVisiting = reader.GetString(3),
                            Citizen = reader.GetBoolean(4),
                            SignedIn = reader.GetBoolean(5),
                            SignedInTime = reader.GetDateTime(6),
                            VisitorID = reader.GetInt32(7)
                        });
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return signedOutRecords;
        }
    }
}
