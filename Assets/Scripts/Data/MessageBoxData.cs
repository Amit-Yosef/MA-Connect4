using System;

namespace Data
{
    public struct MessageBoxData
    {
        public string Title;
        public string Body;
        public Action OnClick;

        public class Builder
        {
            private string _title;
            private string _body = String.Empty;
            private Action _onClick;

            public Builder WithTitle(string title)
            {
                _title = title;
                return this;
            }

            public Builder WithBody(string body)
            {
                _body = body;
                return this;
            }

            public Builder WithOnClick(Action onClick)
            {
                _onClick = onClick;
                return this;
            }

            public MessageBoxData Build()
            {
                return new MessageBoxData
                {
                    Title = _title,
                    Body = _body,
                    OnClick = _onClick
                };
            }
        }
    }
}