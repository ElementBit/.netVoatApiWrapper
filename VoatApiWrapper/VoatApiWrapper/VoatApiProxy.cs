﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VoatApiWrapper
{

    //This isn't a complete coverage
    public class VoatApiProxy : BaseApiProxy {

        #region Submission
        public ApiResponse SubmitDiscussion(string subverse, string title, string content) {
            return Request(HttpMethod.Post, String.Format("api/v1/v/{0}", subverse), new { title = title, content = content });
        }

        public ApiResponse SubmitLink(string subverse, string title, string url) {
            return Request(HttpMethod.Post, String.Format("api/v1/v/{0}", subverse), new { title = title, url = url });
        }

        public ApiResponse EditLinkSubmission(int submissionID, string title, string url) {
            return Request(HttpMethod.Put, String.Format("api/v1/submissions/{0}", submissionID.ToString()), new { title = title, url = url});
        }

        public ApiResponse EditDiscussionSubmission(int submissionID, string title, string content) {
            return Request(HttpMethod.Put, String.Format("api/v1/submissions/{0}", submissionID.ToString()), new { title = title, content = content });
        }

        public ApiResponse DeleteSubmission(int submissionID) {
            return Request(HttpMethod.Delete, String.Format("api/v1/submissions/{0}", submissionID.ToString()));
        }

        #endregion

        #region Subverses 
        
        public ApiResponse GetSubverseInfo(string subverse) {
            return Request(HttpMethod.Get, String.Format("api/v1/v/{0}/info", subverse));
        }
        public ApiResponse BlockSubverse(string subverse) {
            return Request(HttpMethod.Post, String.Format("api/v1/v/{0}/block", subverse));
        }
        public ApiResponse UnblockSubverse(string subverse) {
            return Request(HttpMethod.Delete, String.Format("api/v1/v/{0}/block", subverse));
        }
        
        #endregion 

        #region Get Submissions

        public ApiResponse GetSubmissionsBySubverse(string subverse, object searchOptions) {
            return Request(HttpMethod.Get, String.Format("api/v1/v/{0}", subverse), null, searchOptions);
        }

        public ApiResponse GetSubmissionsAll(object searchOptions) {
            return Request(HttpMethod.Get, String.Format("api/v1/v/_all"), null, searchOptions);
        }

        public ApiResponse GetSubmissionsDefault(object searchOptions) {
            return Request(HttpMethod.Get, String.Format("api/v1/v/_default"), null, searchOptions);
        }

        public ApiResponse GetSubmissionsFront(object searchOptions) {
            return Request(HttpMethod.Get, String.Format("api/v1/v/_front"), null, searchOptions);
        }

        public ApiResponse GetSubmission(int submissionID) {
            return Request(HttpMethod.Get, String.Format("api/v1/submissions/{0}", submissionID.ToString()));
        }
        #endregion

        #region Comments

        public ApiResponse GetComments(string subverse, int submissionID) {
            return Request(HttpMethod.Get, String.Format("api/v1/v/{0}/{1}/comments", subverse, submissionID.ToString()));
        }
        public ApiResponse GetComment(int commentID) {
            return Request(HttpMethod.Get, String.Format("api/v1/comments/{0}", commentID.ToString()));
        }
        public ApiResponse PostComment(string subverse, int submissionID, string comment) {
            return Request(HttpMethod.Post, String.Format("api/v1/v/{0}/{1}/comment", subverse, submissionID.ToString()), new { value = comment });
        }
        public ApiResponse PostCommentReply(int commentID, string comment) {
            return Request(HttpMethod.Post, String.Format("api/v1/comments/{0}", commentID.ToString()), new { value = comment });
        }
        public ApiResponse EditComment(int commentID, string comment) {
            return Request(HttpMethod.Put, String.Format("api/v1/comments/{0}", commentID.ToString()), new { value = comment });
        }
        public ApiResponse DeleteComment(int commentID) {
            return Request(HttpMethod.Delete, String.Format("api/v1/comments/{0}", commentID.ToString()));
        }

        #endregion

        #region Voting


        public ApiResponse VoteComment(int commentID, Vote vote, bool revokeOnRevote = true) {
            return Request(HttpMethod.Post, String.Format("api/v1/vote/{0}/{1}/{2}", "comment", commentID.ToString(), (int)vote), null, new { revokeOnRevote = revokeOnRevote });
        }

        public ApiResponse VoteSubmission(int submissionID, Vote vote, bool revokeOnRevote = true) {
            return Request(HttpMethod.Post, String.Format("api/v1/vote/{0}/{1}/{2}", "submission", submissionID.ToString(), (int)vote), null, new { revokeOnRevote = revokeOnRevote });
        }
        
        #endregion

        #region Saving


        public ApiResponse SaveSubmission(int submissionID) {
            return Request(HttpMethod.Post, String.Format("api/v1/submissions/{0}/save", submissionID.ToString()));
        }
        public ApiResponse UnsaveSubmission(int submissionID) {
            return Request(HttpMethod.Delete, String.Format("api/v1/submissions/{0}/save", submissionID.ToString()));
        }
        public ApiResponse SaveComment(int commentID) {
            return Request(HttpMethod.Post, String.Format("api/v1/comments/{0}/save", commentID.ToString()));
        }
        public ApiResponse UnsaveComment(int commentID) {
            return Request(HttpMethod.Delete, String.Format("api/v1/comments/{0}/save", commentID.ToString()));
        }

        
        #endregion


        #region User 


        public ApiResponse GetUserProfile(string userName) {
            return Request(HttpMethod.Get, String.Format("api/v1/u/{0}/info", userName));
        }
        
        public ApiResponse GetUserComments(string userName, object search) {
            return Request(HttpMethod.Get, String.Format("api/v1/u/{0}/comments", userName), null, search);
        }
        
        public ApiResponse GetUserSubmissions(string userName, object search) {
            return Request(HttpMethod.Get, String.Format("api/v1/u/{0}/submissions", userName), null, search);
        }

        #endregion 



    }
}
