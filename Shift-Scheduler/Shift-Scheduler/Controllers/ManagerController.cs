using Shift_Scheduler.ViewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shift_Scheduler.Models
{
    // todo: only allow manager
    public class ManagerController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        enum Days { Sunday, Monday, Tuesday, Wednesday, Thursday, Friday, Saturday };

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Shift()
        {
            ViewBag.shift = db.Shifts.ToList();

            return View();
        }

        public JsonResult GetShifts()
        {
            List<KeyValuePair<string, List<ScheduleEmp>>> output = new List<KeyValuePair<string, List<ScheduleEmp>>>();

            var vac = (from e in db.Employees
                       from v in e.vacationRequests
                       where v.approvalStatus == "approved" && (v.dateStart <= DateTime.Now && v.dateEnd >= DateTime.Now)
                       select e.employeeId).ToList();

            foreach (var shift in db.Shifts)
            {                
                var res = (from e in db.Employees
                           from s in e.shifts
                           where s.shiftId == shift.shiftId
                           select new { e.employeeId, e.firstName, e.lastName, e.phoneNumber }).ToList();

                for(int i=res.Count-1;i>= 0;i--)
                {
                    if (vac.Contains(res[i].employeeId))
                        res.RemoveAt(i);
                }

                List<ScheduleEmp> temp = new List<ScheduleEmp>();
                foreach (var result in res)
                {
                    temp.Add(new ScheduleEmp(result.employeeId, result.firstName, result.lastName, result.phoneNumber));
                }

                output.Add(new KeyValuePair<string, List<ScheduleEmp>>(shift.shiftId, temp));
            }

            return Json(output, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostShifts(string startDate, ShiftEmp[] data)
        {
            var shifts = (from s in db.Shifts
                          select s.shiftId).ToList();

            foreach (ShiftEmp shift in data)
            {
                if (shift.empId == null || !shifts.Contains(shift.shiftId))
                    return Json(new { error = "error" });
                else
                {
                    var day = (from s in db.Shifts
                               where s.shiftId == shift.shiftId
                               select new { s.dayOfTheWeek, s.shiftType }).FirstOrDefault();

                    Days dayval;
                    if (Enum.TryParse(day.dayOfTheWeek, out dayval))
                    {
                        DateTime start;
                        try
                        {
                            start = Convert.ToDateTime(startDate);
                        }
                        catch (FormatException)
                        {
                            return Json(new { error = "error" });
                        }

                        start = start.AddDays((int)dayval);

                        var exist = (from s in db.ShiftSchedules
                                     where s.date == start && s.shiftType == day.shiftType
                                     select s).FirstOrDefault();

                        int res;
                        if (!Int32.TryParse(shift.empId, out res))
                            return Json(new { error = "error" });

                        if (exist != null)
                        {
                            exist.empShiftScheduleID = res;
                            db.Entry(exist).State = System.Data.Entity.EntityState.Modified;

                        }
                        else
                        {
                            ShiftSchedule schedule = new ShiftSchedule();
                            schedule.date = start;
                            schedule.dayOfTheWeek = day.dayOfTheWeek;
                            schedule.empShiftScheduleID = res;
                            schedule.shiftType = day.shiftType;

                            db.ShiftSchedules.Add(schedule);
                        }

                        db.SaveChanges();
                    }
                    else
                    {
                        return Json(new { error = "error" });
                    }
                }
            }

            return Json(new { success = "success" });
        }

        public ActionResult employeeList()
        {
            return View(db.Employees.ToList());
        }

        public ActionResult dashBoard()
        {
            
            int dateNumber = (int)DateTime.Today.DayOfWeek;
            string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            string[] dayId = { "MonMor", "MonEve", "MonNit", "TuesMor", "TuesEve", "TuesNit", "WedMor", "WedEve", "WedNit",
                                 "ThurMor", "ThuEve", "ThuNit", "FriMor", "FriEve", "FriNit", "SatMor", "SatEve", "SatNit",
                                 "SunMor", "SunEve", "SunNit" };
            string[] dayType = { "Morning", "Evening", "Night" };
            string dayOfTheWeek = "";

            ArrayList first = new ArrayList();
            ArrayList last = new ArrayList();
            ArrayList approve = new ArrayList();
            ArrayList vId = new ArrayList();
            ArrayList startDate = new ArrayList();
            ArrayList endDate = new ArrayList();
            ArrayList eId = new ArrayList();
            //int counter = 0;

            for (int i = 0; i < days.Length; i++)
            {
                if (dateNumber == i)
                {
                    dayOfTheWeek = days[i];
                }
            }
            var res = from e in db.Employees
                      from s in e.shifts
                      where s.dayOfTheWeek == "Monday"
                      select e;
            ViewBag.empAvail = res.ToList();
            var res3 = (from v in db.VacationRequests
                       join e in db.Employees on v.employeeId equals e.employeeId
                       select new { e.firstName, e.lastName, v.employeeId,v.dateStart,v.dateEnd,v.approvalStatus,v.vacationID }).ToArray();
            for(int i = 0; i < res3.Length; i++)
            {
                string temp = res3[i].ToString();
                string trim = temp.Trim(new Char[] { '{', '}' });
                string[] split = trim.Split(',');
                int j = 0;
                string[] firstName = split[j++].Split('=');
                string[] lastName = split[j++].Split('=');
                string[] employeeId = split[j++].Split('=');
                string[] dateStart = split[j++].Split('=');
                string[] dateEnd = split[j++].Split('=');
                string[] approvalStatus = split[j++].Split('=');
                string[] vacationId = split[j++].Split('=');

                first.Add(firstName[1].Trim());
                last.Add(lastName[1].Trim());
                eId.Add(employeeId[1].Trim());
                approve.Add(approvalStatus[1].Trim());
                startDate.Add(dateStart[1].Trim());
                endDate.Add(dateEnd[1].Trim());
                vId.Add(vacationId[1].Trim());


            }
 
            ViewBag.fName = first;
            ViewBag.lName = last;
            ViewBag.emId  = eId;
            ViewBag.vaId = vId;
            ViewBag.sDate = startDate;
            ViewBag.eDate = endDate;
            ViewBag.app = approve;
            ViewBag.vacation = res3.ToList();

            foreach (var s in db.shiftChangeRequest)
            {
                var res2 = (from sc in db.shiftChangeRequest
                            from e in db.Employees
                            where e.employeeId == s.currentWorkingEmp.employeeId
                            select new DashBoardViewModel
                            {
                                shiftChangeRequestId = sc.shiftChangeRequestId,
                                shiftApproval = sc.shiftApproval,
                                shiftScheduleID = sc.shiftScheduleID,
                                currentWorkingEmpFirstName = sc.currentWorkingEmp.firstName,
                                currentWorkingEmpLastName = sc.currentWorkingEmp.lastName,
                                newWorkingEmpFirstName = sc.newWorkingEmp.firstName,
                                newWorkingEmpLastName = sc.currentWorkingEmp.lastName,
                                currentWorkingEmpId = sc.currentWorkingEmp.employeeId,
                                newWorkingEmpId = sc.newWorkingEmp.employeeId,

                            }).ToList();
               
                return View(res2);

            }

            return View();
        }
            

        public ActionResult Report()
        {
            ViewBag.shift = db.Shifts.ToList();

            return View();
        }

        public JsonResult GetShiftSchedule()
        {
            DateTime startOfWeek = DateTime.Today.AddDays(-1 * (int)(DateTime.Today.DayOfWeek));
            DateTime endOfWeek = startOfWeek.AddDays(6);

            var result = (from s in db.ShiftSchedules
                          from e in db.Employees
                          where s.date >= startOfWeek && s.date <= endOfWeek && s.empShiftScheduleID == e.employeeId
                          select new { s.dayOfTheWeek, s.shiftType, e.firstName, e.lastName }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangeShift()
        {
            return View();
        }

        public ActionResult VacationRequest()
        {
            ViewData["Vacation"] = (from v in db.VacationRequests
                                    where v.approvalStatus == "pending"
                                    select v).ToList();

            return View();
        }

        public ActionResult VacationApprove(int id)
        {
            var res = (from v in db.VacationRequests
                       where v.vacationID == id
                       select v).FirstOrDefault();

            if (res != null)
            {
                res.approvalStatus = "approved";                               
                db.Entry(res).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("VacationRequest");
        }

        public ActionResult VacationDeny(int id)
        {
            var res = (from v in db.VacationRequests
                       where v.vacationID == id
                       select v).FirstOrDefault();

            if (res != null)
            {
                res.approvalStatus = "denied";
                db.Entry(res).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("VacationRequest");
        }

        public JsonResult GetShiftChange()
        {
            var res = (from c in db.shiftChangeRequest
                       from s in db.ShiftSchedules
                       from e in db.Employees
                       where c.shiftScheduleID == s.shiftScheduleId && c.newWorkingEmp.employeeId == e.employeeId && c.shiftApproval == "pending"
                       select new shiftChangeEmp { id = c.shiftChangeRequestId, date = s.date, shiftType = s.shiftType, curFirstName = c.currentWorkingEmp.firstName, curLastName = c.currentWorkingEmp.lastName, newFirstName = e.firstName, newLastName = e.lastName }).ToList();

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RequestApprove(int id)
        {
            var res = (from c in db.shiftChangeRequest
                       where c.shiftChangeRequestId == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.shiftApproval = "approved";
                var result = (from s in db.ShiftSchedules
                              where res.shiftScheduleID == s.shiftScheduleId
                              select s).FirstOrDefault();

                result.empShiftScheduleID = res.newWorkingEmp.employeeId;

                db.Entry(result).State = EntityState.Modified;
                db.Entry(res).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("ChangeShift");
        }

        public ActionResult RequestDeny(int id)
        {
            var res = (from c in db.shiftChangeRequest
                       where c.shiftChangeRequestId == id
                       select c).FirstOrDefault();

            if (res != null)
            {
                res.shiftApproval = "denied";
                db.Entry(res).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("ChangeShift");
        }
    }

    public class shiftChangeEmp
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public string shiftType { get; set; }
        public string curFirstName { get; set; }
        public string curLastName { get; set; }
        public string newFirstName { get; set; }
        public string newLastName { get; set; }
    }

    public class ShiftEmp
    {
        public string empId { get; set; }
        public string shiftId { get; set; }
    }

    public class ScheduleEmp
    {
        private int id;
        private string firstname;
        private string lastname;
        private string phone;

        public ScheduleEmp(int id, string firstname, string lastname, string phone)
        {
            this.id = id;
            this.firstname = firstname;
            this.lastname = lastname;
            this.phone = phone;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }
        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

    }
}