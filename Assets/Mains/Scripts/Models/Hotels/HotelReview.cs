using System;
using System.Collections.Generic;
using UnityEngine;

namespace YNL.Checkotel
{
    [System.Serializable]
    public class HotelReview
    {
        public ReviewRating AverageRating
        {
            get
            {
                ReviewRating rating = 0;

                foreach (var feedback in Feedbacks)
                {
                    rating += feedback.Rating;
                }

                if (Feedbacks.Count > 0)
                {
                    return rating / Feedbacks.Count;
                }

                return -1;
            }
        }

        public List<ReviewFeedback> Feedbacks = new();
    }

    [System.Serializable]
    public struct ReviewRating : IComparable<ReviewRating>
    {
        [SerializeField] private float _rating;

        public static implicit operator float(ReviewRating rating) => rating._rating;
        public static implicit operator ReviewRating(float rating) => new() { _rating = rating };

        public int CompareTo(ReviewRating other)
        {
            return _rating.CompareTo(other._rating);
        }
    }

    [System.Serializable]
    public class ReviewFeedback
    {
        public UID CustomerID;
        public ReviewRating Rating = 0;
        public string Comment = string.Empty;
    }
}
