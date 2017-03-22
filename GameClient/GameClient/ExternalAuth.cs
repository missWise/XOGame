using Nemiro.OAuth.LoginForms;
using System;
using System.Collections;
using System.Collections.Generic;

namespace GameClient
{
    class ExternalAuth
    {
        public ExternalAuth()
        {

        }

        public string Google_Auth()
        {
            string info = null;

            var login = new GoogleLogin("991072998353-0kno6kh8b0hnhq2tuplall1kl904oaga.apps.googleusercontent.com", "CQyfo4pc-jy6b-QJS3wacy3j", "https://www.googleapis.com/auth/plus.login", autoLogout: true, loadUserInfo: true);
            login.ShowDialog();

            if (login.IsSuccessfully)
            {
                info = login.UserInfo.FirstName;
            }
            return info;
        }

        public string Facebook_Auth()
        {
            string info = null;

            var login = new FacebookLogin("1435890426686808", "c6057dfae399beee9e8dc46a4182e8fd", autoLogout: true, loadUserInfo: true);
            login.ShowDialog();

            if (login.IsSuccessfully)
            {
                info = login.UserInfo.FirstName;
            }

            return info;
        }
    }
}