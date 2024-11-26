
using ex2.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Net.Mail;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace ex2.Repositories
{

    public class TasksRepository : ITasksRepository
    {
        private readonly TasksContext _context;
        string Cnn;
        public TasksRepository(TasksContext context, IConfiguration configuration)
        {
            _context = context;
            Cnn = configuration.GetConnectionString("DefaultConnection");
        }

        public bool add(Tasks task)
        {
            var user = _context.Users.Where(x => x.Id == task.UserId);
            var project = _context.Projects.Where(x => x.Id == task.ProjectId);
            if (user.Count() > 0 && project.Count() > 0)
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public void delete(Tasks task)
        {
            _context.Tasks.Remove(task);
            _context.SaveChanges();
        }

        public IEnumerable<Tasks> Get()
        {
            return _context.Tasks.ToList();
        }


        public void update(Tasks task)
        {
            _context.Tasks.Update(task);
            _context.SaveChanges();
        }
        public IEnumerable<Tasks> GetTaskByUser(int Id){
            var tasks = new List<Tasks>();
            foreach (var t in _context.Tasks)
            {
                if (t.UserId == Id)
                    tasks.Add(t);
            }
            return tasks;
        }
        //public DataTable addAttachment(Attachment attachment)
        //{

        //    DataTable dt = new DataTable();

        //    using (SqlConnection connection = new SqlConnection(Cnn))
        //    {
        //        using (SqlCommand command = new SqlCommand("Attachments_AddAttachment", connection))
        //        {
        //            command.CommandText = "Attachments_AddAttachment";
        //            command.CommandType = CommandType.StoredProcedure;

        //            command.Parameters.AddWithValue("@Name", attachment.Name);

        //            int rowsAffact = command.ExecuteNonQuery();

        //            connection.Open();

        //            using (SqlDataAdapter da = new SqlDataAdapter(command))
        //            {
        //                da.Fill(dt);

        //            }
        //        }
        //    }
        //    return dt;
        //}
    //    public DataTable getByProject(int ProjactId)
    //    {
    //        DataTable dt = new DataTable();

    //        using (SqlConnection connection = new SqlConnection(Cnn))
    //        {
    //            using (SqlCommand command = new SqlCommand())
    //            {
    //                //commandtype
    //                command.CommandText = "getByProject";
    //                command.CommandType = CommandType.StoredProcedure;

    //                //paramters
    //                SqlParameter sqlParameter = new SqlParameter("@ProjactId", ProjactId);

    //                command.Parameters.Add(sqlParameter);

    //                connection.Open();

    //                using (SqlDataAdapter da = new SqlDataAdapter(command))
    //                {
    //                    da.Fill(dt);

    //                }
    //            }
    //        }
    //        return dt;
    //    }
    }
   
}


//var user
//var project

// if (task.UserId != 0)
// {
//     user = _context.Users.Where(x => x.Id == task.UserId);
// }

// if (task.ProjectId != 0)
// {
//      project = _context.Projects.Where(x => x.Id == task.ProjectId);
// }

// if (task.UserId != 0)
// {

// }
