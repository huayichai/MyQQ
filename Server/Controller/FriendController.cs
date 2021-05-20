using Server.DAO;
using Server.Entity;
using Server.Router._Request;
using Server.Router._Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Controller
{
    class FriendController
    {
        public static List<Friend> GetAllFriends(string account)
        {
            List<Friend> friends = UserDAO.SelectFriends(account);
            friends.AddRange(UserDAO.SelectGroup(account));
            return friends;
        }

        public static Friend SearchFriend(string account)
        {
            User user = UserDAO.SelectUserByAccount(account);
            if (user != null)
            {
                Friend friend = new Friend(user);
                return friend;
            }
            else
            {
                return null;
            }

        }

        public static void AddFriendRequest(AddFriendRequest request)
        {
            if (CenterController.clientMap.ContainsKey(request.friendAccount))
            {
                request.SetFriend(SearchFriend(request.userAccount)); // 找到请求者信息              
                Client client = CenterController.clientMap[request.friendAccount];
                client.socket.Send(request.ToString());
            }
        }

        public static void AddFriendResponse(AddFriendResponse response)
        {
            if (CenterController.clientMap.ContainsKey(response.friendAccount))
            {
                response.SetFriend(SearchFriend(response.userAccount));
                Client client = CenterController.clientMap[response.friendAccount];
                client.socket.Send(response.ToString());
            }
            UserDAO.InsertFriend(response.userAccount, response.friendAccount);
            UserDAO.InsertFriend(response.friendAccount, response.userAccount);
        }
    }
}
