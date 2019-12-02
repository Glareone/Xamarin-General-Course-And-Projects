using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DeliveriesApp.Model;
using Java.Lang;

namespace DeliveriesApp.Droid
{
    public class DeliveryAdapter : BaseAdapter
    {
        private readonly Context context;
        private readonly List<Delivery> deliveries;

        public DeliveryAdapter(Context context, List<Delivery> deliveries)
        {
            this.context = context;
            this.deliveries = deliveries;
        }

        public override int Count => deliveries.Count;


        public override Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;
            DeliveryAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as DeliveryAdapterViewHolder;

            if (holder == null)
            {
                holder = new DeliveryAdapterViewHolder();
                var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
                //replace with your item and your holder items
                //comment back in
                view = inflater.Inflate(Resource.Layout.DeliveryCell, parent, false);

                //define how our cell should look like.
                // by default template contains only "Title" property. We replace it with our two props from the DeliveryCell - Name and Status.
                holder.Name = view.FindViewById<TextView>(Resource.Id.deliveryNameTextView);
                holder.Status = view.FindViewById<TextView>(Resource.Id.deliveryStatusTextView);

                view.Tag = holder;
            }


            //fill in your items
            var currentDeliveryByPosition = deliveries[position];
            holder.Name.Text = currentDeliveryByPosition.Name;

            switch (currentDeliveryByPosition.Status)
            {
                case 0:
                    holder.Status.Text = "Waiting delivery person";
                    break;
                case 1:
                    holder.Status.Text = "Being delivered";
                    break;
                case 2:
                    holder.Status.Text = "Delivered";
                    break;
            }

            return view;
        }
    }

    internal class DeliveryAdapterViewHolder : Object
    {
        //Your adapter views to re-use
        public TextView Name { get; set; }

        public TextView Status { get; set; }
    }
}