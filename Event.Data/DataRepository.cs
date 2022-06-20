using System;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using Event.Common.Enum;
using Event.Common.Models;
using Event.Common.Utility;

namespace Event.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly IConfiguration _configuration;
        public DataRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(Encryptor.DecryptString(_configuration.GetConnectionString("Event")));
        }

        public void AddVenueToConvention(AddVenueServiceModel model, CancellationToken httpContextRequestAborted)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "spAddVenueToConvention",
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.AddWithValue("ConventionId", model.ConventionId);
                cmd.Parameters.AddWithValue("VenueId", model.Venue.VenueId);
                cmd.Parameters.AddWithValue("Name", model.Venue.Name);
                cmd.Parameters.AddWithValue("City", model.Venue.City);
                cmd.Parameters.AddWithValue("PostalCode", model.Venue.PostalCode);

                cmd.ExecuteNonQuery();
            }
        }

        public int CreateConvention(string conventionName, CancellationToken httpContextRequestAborted)
        {
            var conventionId = -1;
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "spCreateConvention",
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.AddWithValue("ConventionName", conventionName);
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        conventionId = Convert.ToInt32(reader["ID"]);
                    }
                }
            }
            return conventionId;
        }

        public void CreateTalk(TalkServiceModel model, CancellationToken httpContextRequestAborted)
        {
            using (var conn = GetConnection())
            {

                conn.Open();
                var cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "spCreateTalk",
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.AddWithValue("ConventionId", model.ConventionId);
                cmd.Parameters.AddWithValue("VenueId", model.VenueId);
                cmd.Parameters.AddWithValue("UserId", model.UserId);
                cmd.Parameters.AddWithValue("Topic", model.Topic);
                cmd.Parameters.AddWithValue("Info", model.Info);
                
                cmd.ExecuteNonQuery();
            }
        }

        public void RegisterSeat(RegisterSeatServiceModel model, CancellationToken httpContextRequestAborted)
        {
            using (var conn = GetConnection())
            {

                conn.Open();
                var cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "spRegisterSeat",
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.AddWithValue("UserID", model.UserId);
                cmd.Parameters.AddWithValue("TalkID", model.TalkId);
                
                cmd.ExecuteNonQuery();
            }
        }

        public UserServiceModel RegisterUser(UserServiceModel model, CancellationToken httpContextRequestAborted)
        {
            //Don't register the user if he exists
            var user = GetUser(model, httpContextRequestAborted); ;
            if (user != null)
            {
                return user;
            }

            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "spRegisterUser",
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.AddWithValue("IdentityId", model.IdentityId);
                cmd.Parameters.AddWithValue("IdentityName", model.IdentityName);
                cmd.Parameters.AddWithValue("UserRole", model.UserRole);
                cmd.Parameters.AddWithValue("Name", model.Name);
                cmd.Parameters.AddWithValue("Email", model.Email);
                cmd.Parameters.AddWithValue("Address", model.Address);
                cmd.Parameters.AddWithValue("PhoneNumber", model.PhoneNumber);

                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return GetUser(model, httpContextRequestAborted);
        }

        public UserServiceModel GetUser(UserServiceModel model, CancellationToken httpContextRequestAborted)
        {
            UserServiceModel user = null;
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "spGetUser",
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.AddWithValue("identId", model.IdentityId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new UserServiceModel
                        {
                            Id = Convert.ToInt32(reader["ID"]),
                            IdentityId = reader["IdentityId"].ToString(),
                            IdentityName = reader["IdentityName"].ToString(),
                            UserRole = (UserRole)Convert.ToInt32(reader["UserRoleID_FK"]),
                            Name = reader["Name"].ToString(),
                            Address = reader["Address"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                        };
                    }
                }
                conn.Close();
            }
            return user;
        }

        public void SignUpForConvention(SignUpServiceModel model, CancellationToken httpContextRequestAborted)
        {
            using (MySqlConnection conn = GetConnection())
            {

                conn.Open();
                var cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "spSignUpForConvention",
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.AddWithValue("ConventionId", model.ConventionId);
                cmd.Parameters.AddWithValue("VenueId", model.VenueId);
                cmd.Parameters.AddWithValue("UserId", model.UserId);

                cmd.ExecuteNonQuery();
            }
        }

        public List<Convention> GetConventions(CancellationToken httpContextRequestAborted)
        {
            var list = new List<Convention>();
            using (var conn = GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "spGetConventions",
                    CommandType = CommandType.StoredProcedure,
                };

                using (var reader = cmd.ExecuteReader())
                {
                    list = MapConvention(reader);
                }
                conn.Close();
            }
            return list;


        }
        public List<Convention> GetMyConventions(UserServiceModel user, CancellationToken httpContextRequestAborted)
        {
            List<Convention> list;
            using (var conn = GetConnection())
            {

                conn.Open();
                var cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = "spGetMyConventions",
                    CommandType = CommandType.StoredProcedure,
                };
                cmd.Parameters.AddWithValue("UserID", user.Id);

                using (var reader = cmd.ExecuteReader())
                {
                    list = MapConvention(reader);

                }
                conn.Close();
            }

            return list;
        }

        private List<Convention> MapConvention(MySqlDataReader reader)
        {
            var list = new List<Convention>();
            Convention convention = null;
            Venue venue = null;
            while (reader.Read())
            {
                var conventionId = Convert.ToInt32(reader["ConventionId"]);
                var conventionInList = list.FirstOrDefault(t => t.Id == conventionId);
                if (conventionInList != null)
                {
                    convention = conventionInList;
                }
                else
                {
                    convention = new Convention
                    {
                        Id = conventionId,
                        Name = reader["ConventionName"].ToString(),
                        Venues = new List<Venue>()
                    };
                    list.Add(convention);
                }

                var venueStringId = reader["Id"].ToString();
                if (!string.IsNullOrEmpty(venueStringId))
                {
                    var venueId = Convert.ToInt32(venueStringId);
                    var venueInList = convention.Venues.FirstOrDefault(t => t.Id == venueId);
                    if (venueInList != null)
                    {
                        venue = venueInList;
                    }
                    else
                    {
                        venue = new Venue()
                        {
                            Id = venueId,
                            VenueId = reader["VenueId"].ToString(),
                            Name = reader["VenueName"].ToString(),
                            City = reader["VenueCity"].ToString(),
                            PostalCode = reader["VenuePostalCode"].ToString(),
                            Talks = new List<Talk>()
                        };
                        convention.Venues.Add(venue);
                    }

                    var talkId = reader["TalkId"].ToString();
                    if (!string.IsNullOrEmpty(talkId))
                    {
                        var fieldNames = Enumerable.Range(0, reader.FieldCount).Select(i => reader.GetName(i)).ToArray();
                        string? hasReservedSeat = null;
                        if(fieldNames.Contains("HasReservedSeat")){
                            hasReservedSeat = reader["HasReservedSeat"].ToString();
                        }
                        var talk = new Talk()
                        {
                            Id = Convert.ToInt32(talkId),
                            Speaker = reader["Speaker"].ToString(),
                            Topic = reader["Topic"].ToString(),
                            Info = reader["Info"].ToString(),
                            HasReservedSeat = !String.IsNullOrEmpty(hasReservedSeat)
                            
                        };
                        venue.Talks.Add(talk);
                    }
                }

            }

            return list;
        }
    }
}
