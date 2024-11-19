using System;
using UnityEngine;

namespace Project.Data.Models
{
    public struct MessageBoxData
    {
        public string Title;
        public string Body;
        public Sprite Image;
        public Action OnClickConfirm;
        public Action OnClickBackArrow;
        public bool ShouldImageTween;

        public class Builder
        {
            private string _title = string.Empty;
            private string _body = string.Empty;
            private Sprite _image;
            private Action _onClickConfirm;
            private Action _onClickBackArrow;
            private bool _shouldImageTween = false;

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

            public Builder WithImage(Sprite image)
            {
                _image = image;
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

            public Builder TweenImage()
            {
                _shouldImageTween = true;
                return this;
            }

            public MessageBoxData Build()
            {
                return new MessageBoxData
                {
                    Title = _title,
                    Body = _body,
                    Image = _image,
                    OnClickConfirm = _onClickConfirm,
                    OnClickBackArrow = _onClickBackArrow,
                    ShouldImageTween = _shouldImageTween
                };
            }
        }
    }
}