using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewScrollToDemo
{
    public class MessageChatCell : ViewCell
    {

        public MessageChatCell()
        {
            var messageTextLabel = new Label
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
            };

            var sendDateLabel = new Label
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.Black,
            };

            var messageGrid = new Grid();

            messageGrid.BackgroundColor = Color.Gray;
            messageGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Star) });
            messageGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100, GridUnitType.Star) });
            messageGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(30, GridUnitType.Absolute) });

            messageTextLabel.SetBinding(Label.TextProperty, new Binding("Text"));
            sendDateLabel.SetBinding(Label.TextProperty, new Binding("SendDateString"));

            messageGrid.Padding = new Thickness(10);
            messageGrid.Children.Add(messageTextLabel, 0, 0);
            messageGrid.Children.Add(sendDateLabel, 0, 1);

            View = new ContentView
            {
                Content = messageGrid
            };
        }

    }
}
