using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace XO_socket.Droid
{
    class AdapterXO : BaseAdapter
    {

        Context context;
        LayoutInflater layoutInflater;
        List<string> nameList;

        public AdapterXO(Context context, List<string> nameList)
        {
            this.context = context;
            layoutInflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            this.nameList = nameList;
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return nameList.ElementAt(position);
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = layoutInflater.Inflate(Resource.Layout.lobby_list_item, parent, false);
            }

            TextView tvName = (TextView)view.FindViewById(Resource.Id.lobby_list_item_tvName);
            tvName.Text = nameList.ElementAt(position);

            tvName.LongClick += delegate
            {
                InvitationBox(tvName.Text.ToString());
            };

            return view;
        }

        private void InvitationBox(string playerName)
        {
            AlertDialog.Builder aDialogBuilder = new AlertDialog.Builder(context);
            aDialogBuilder.SetMessage("Invitation");
            aDialogBuilder.SetMessage("Do you want to play with " + playerName + "?");



            aDialogBuilder.SetPositiveButton("Yes", (sender, args) => {
                Invite(playerName);
                (sender as Dialog).Cancel();
            });
            aDialogBuilder.SetNegativeButton("No", (sender, args) =>
            {
                (sender as Dialog).Cancel();
            });

            aDialogBuilder.SetCancelable(false);
            aDialogBuilder.Create();
            aDialogBuilder.Show();
        }
        //Fill in cound here, currently 0
        public override int Count
        {
            get
            {
                return nameList.Count;
            }
        }
        private void Invite(string playerName)
        {
            MyFragmentManager.SendInviteRequest(playerName);
        }

    }

}