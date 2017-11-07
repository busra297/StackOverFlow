using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace sof_github_wcf1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Sof_Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Sof_Service.svc or Sof_Service.svc.cs at the Solution Explorer and start debugging.
    public class Sof_Service : ISof_Service
    {
        StackOverFlowEntities database = new StackOverFlowEntities();

        public static List<Questions> questions = new List<Questions>();

        public List<User> UserTurn()
        {
            List<User> User_information = new List<User>();

            foreach (User u in database.User)
            {
                User user = new User();
                user.UserName = u.UserName;
                user.UserID = u.UserID;
                user.Password = u.Password;
                User_information.Add(user);

            }
            return User_information;
        }

   



        public User UserControl(string username, string password)
        {
            User u = new User();
            foreach (User item in database.User)
            {
                if ((item.UserName == username) && (item.Password == password))
                {

                    u.UserName = item.UserName;
                    u.Password = item.Password;
                    u.UserID = item.UserID;

                }


            }
            return u;
        }

        public void UserAdd(string username, string password, string email)
        {
            User u = new User();
            u.UserName = username;
            u.Password = password;
            u.EMail = email;
            database.User.Add(u);
            database.SaveChanges();
        }


        public void QuestionAdd(string question, int UserID)
        {
            Questions q = new Questions();
            q.Question = question;
            q.UserID = UserID;
            database.Questions.Add(q);
            database.SaveChanges();
        }


        public List<Questions> QuestionsList()
        {
            questions.Clear();


            foreach (Questions q in database.Questions)
            {
                Questions question = new Questions();
                question.QuestionID = q.QuestionID;
                question.Question = q.Question;
                question.UserID = q.UserID;
                question.Question_Favorite = q.Question_Favorite;
                questions.Add(question);




            }

            return questions;
        }



        public void AnswerAdd(int questionID, string answer)
        {
            Answers a = new Answers();
            a.Answer = answer;
            a.QuestionID = questionID;
            database.Answers.Add(a);
            database.SaveChanges();

        }

        public List<Answers> AnswerList(int id)
        {
            foreach (Questions s in questions)
            {

                s.Answers.Clear();

            }

            foreach (Questions s in questions)
            {

                foreach (Answers a in database.Answers)
                {

                    Answers answer = new Answers();

                    answer.AnswerID = a.AnswerID;
                    answer.Answer = a.Answer;
                    answer.Answer_Favorite = a.Answer_Favorite;
                    answer.QuestionID = a.QuestionID;


                    if (answer.QuestionID == s.QuestionID)
                    {
                        s.Answers.Add(answer);
                    }

                }

            }

            return questions.Find(x => x.QuestionID == id).Answers.ToList();
        }

        public void CommentAdd(int answerID, string comment)
        {
            Comments c = new Comments();
            c.AnswerID = answerID;
            c.Comment = comment;
            database.Comments.Add(c);
            database.SaveChanges();
        }

        public List<Comments> CommentList(int questionID, int answerID)
        {
            foreach (Questions q in questions)
            {
                foreach (Answers a in q.Answers)
                {
                    a.Comments.Clear();
                }

            }

            foreach (Questions q in questions)
            {
                foreach (Answers a in q.Answers)
                {
                    foreach (Comments commentitem in database.Comments)
                    {

                        Comments c = new Comments();

                        c.CommentID = commentitem.CommentID;
                        c.Comment = commentitem.Comment;
                        c.AnswerID = commentitem.AnswerID;


                        if (c.AnswerID == a.AnswerID)
                        {
                            a.Comments.Add(c);
                        }

                    }
                }

            }

            List<Answers> answer = questions.Find(x => x.QuestionID == questionID).Answers.ToList();
            return answer.Find(a => a.AnswerID == answerID).Comments.ToList();

        }

        public int LikeNumber(int questionID)
        {
            int questionlikenumber = 0;
            foreach (Questions question in database.Questions)
            {
                Questions q = new Questions();
                q.UserID = question.UserID;
                q.Question = question.Question;
                q.Question_Favorite = question.Question_Favorite;

                if (questionID == question.QuestionID)
                {
                    questionlikenumber = question.Question_Favorite;

                    break;
                }

            }
            return questionlikenumber;
        }
       

    }
}
