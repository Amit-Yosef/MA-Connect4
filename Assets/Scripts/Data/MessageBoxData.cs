using System;

namespace Data
{
    public struct MessageBoxData
    {
        public string Title;
        public string Body;
        public Action OnClickConfirm;
        public Action OnClickBackArrow;

        public class Builder
        {
            private string _title;
            private string _body = String.Empty;
            private Action _onClickConfirm;
            public Action _onClickBackArrow;


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

            public Builder WithOnClickConfirm(Action onClick)
            {
                _onClickConfirm = onClick;
                return this;
            }
            public Builder WithOnClickBackArrow(Action onClick)
            {
                _onClickBackArrow = onClick;
                return this;
            }

            public MessageBoxData Build()
            {
                return new MessageBoxData
                {
                    Title = _title,
                    Body = _body,
                    OnClickConfirm = _onClickConfirm,
                    OnClickBackArrow = _onClickBackArrow
                };
            }
        }
    }
}