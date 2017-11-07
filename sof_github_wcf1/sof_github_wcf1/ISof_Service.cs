using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace sof_github_wcf1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ISof_Service" in both code and config file together.
    [ServiceContract]
    public interface ISof_Service
    {
        [OperationContract]
        List<User> UserTurn();

        [OperationContract]
        List<Questions> QuestionsList();

        [OperationContract]
        List<Answers> AnswerList(int id);

        [OperationContract]
        void UserAdd(string username, string password, string email);

        [OperationContract]
        User UserControl(string username, string password);

        [OperationContract]
        void QuestionAdd(string question, int UserID);

        [OperationContract]
        void AnswerAdd(int questionID, string answer);

        [OperationContract]
        List<Comments> CommentList(int questionID, int answerID);

        [OperationContract]
        void CommentAdd(int answerID, string comment);

        [OperationContract]
        int LikeNumber(int questionID);


       
    }
}
