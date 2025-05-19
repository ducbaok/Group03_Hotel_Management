using UnityEngine;
using UnityEngine.UIElements;

namespace YNL.Checkotel
{
    public partial class InformationViewReviewPage : ViewPageUI
    {
        private VisualElement _backButton;
        private RatingView _ratingView;
        private VisualElement _reviewScroll;

        protected override void Collect()
        {
            _ratingView = new(Root.Q("TopBar").Q("RatingView"));

            _reviewScroll = Root.Q("ContentScroll").Q("unity-content-container");
        }

        protected override void Initialize()
        {
            _reviewScroll.Clear();
            for (byte i = 0; i < 10; i++)
            {
                var reviewItem = new ReviewResultItemUI();
                _reviewScroll.Add(reviewItem);

                if (i == 0) reviewItem.SetAsFirstItem();
            }
        }
    }
}