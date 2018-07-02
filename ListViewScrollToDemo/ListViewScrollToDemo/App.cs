using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace ListViewScrollToDemo
{
    public class App : Application
    {

        private MessageChatViewModel ViewModel
        {
            get { return BindingContext as MessageChatViewModel; }
        }

        public App()
        {

            BindingContext = new MessageChatViewModel();

            var messageList = new ListView
            {
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ItemTemplate = new DataTemplate(typeof(MessageChatCell)),
                HasUnevenRows = true,
            };

            var addMessageButton = new Button
            {
                Text = "Add message",
                HorizontalOptions = LayoutOptions.Fill,
                VerticalOptions = LayoutOptions.End,
                Command = ViewModel.SendMessageButtonCommand,
            };

            MessagingCenter.Subscribe<MessageChatViewModel>(this, "ScrollToEnd", (sender) =>
            {
                var lastItem = messageList.ItemsSource.OfType<object>().Last();

                Device.BeginInvokeOnMainThread(() =>
                {
                    if (lastItem != null)
                    {
                        messageList.ScrollTo(lastItem, ScrollToPosition.End, true);
                    }
                });
            });

            messageList.SetBinding(ListView.ItemsSourceProperty, new Binding("Messages"));

            // The root page of your application
            MainPage = new ContentPage
            {

                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand,

                    Children =
                    {
                        messageList,
                        addMessageButton
                    }
                }

            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }


    public class MessageChatViewModel
    {
        private long messageCounter = 0;

        private ObservableCollection<MessageItem> messages;

        public ObservableCollection<MessageItem> Messages
        {
            get
            {
                return messages;
            }
            set
            {
                messages = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Command sendMessageButtonCommand;

        public Command SendMessageButtonCommand
        {
            get
            {
                return sendMessageButtonCommand ?? (sendMessageButtonCommand = new Command(OnSendButtonCommand));
            }
        }

        private void OnSendButtonCommand()
        {
            Messages.Add(new MessageItem
            {
                Text = "New message " + messageCounter,
                SendDateString = DateTime.Now.ToString()
            });

            messageCounter++;

            MessagingCenter.Send<MessageChatViewModel>(this, "ScrollToEnd");
        }

        public MessageChatViewModel()
        {
            Messages = new ObservableCollection<MessageItem>();
        }


    }


    public class MessageItem
    {
        public string Text { get; set; }
        public string SendDateString { get; set; }
    }

}
