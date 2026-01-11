using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Database;
using Firebase.Database.Query;


using GorselProgramlamaOdev3.Model;

using System;
using System.Collections.Generic;
using System.Text;

namespace GorselProgramlamaOdev3.Services
{
    internal static class FirebaseServices
    {
        static string projectId = "odev3-ac7af";
        static string apiKey = "AIzaSyDqkmXDWIAduuhiimnOARKG0aobvf-4bKg";
        static string authDomain = "odev3-ac7af.firebaseapp.com";
        static string storeBucked = $"{projectId}.appspot.com";


        static FirebaseAuthConfig config = new FirebaseAuthConfig()
        {
            ApiKey = apiKey,
            AuthDomain = authDomain,
            Providers = new FirebaseAuthProvider[] { new EmailProvider() }
        };
        public static Task<bool> Register(string username, string email, string password, ref string message)
        {
            message = "";
            try
            {

                var client = new FirebaseAuthClient(config);
                var res = client.CreateUserWithEmailAndPasswordAsync(email, password, username);

                return Task.FromResult(res.Result.User != null ? true : false);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Task.FromResult(false);
        }

       
        public static async Task<bool> Login(string email, string password, Action<string> setMessage)
        {
            setMessage?.Invoke("");
            try
            {
                var client = new FirebaseAuthClient(config);
                var res = await client.SignInWithEmailAndPasswordAsync(email, password);

                if (res?.User != null)
                {
                    return true;
                }
                else
                {
                    setMessage?.Invoke("Giriþ baþarýsýz");
                    return false;
                }
            }
            catch (Exception ex)
            {
                setMessage?.Invoke(ex.Message);
                return false;
            }
        }

        const string ConnectionString = "https://odev3-ac7af-default-rtdb.firebaseio.com/s";
        static FirebaseClient firebaseClient = new FirebaseClient(ConnectionString);

        internal static bool AddNewTodo(ToDoItem item, ref string message)
        {
            message = "";
            // TODO: Implement Firebase Firestore integration to add a new ToDo item
            if (item == null)
            {
                message = "ToDo item cannot be null.";
                return false;
            }
            else
            {
                firebaseClient.Child($"todos/{item.Id}").PutAsync(item).Wait();
                return true;
            }

        }
    }
}
