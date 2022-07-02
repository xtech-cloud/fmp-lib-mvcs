using XTC.FMP.Lib.MVCS;

namespace TEST_fmp_lib_mvcs
{
    class ConsoleLogger : Logger
    {
        protected override void trace(string _categoray, string _message)
        {
            Console.WriteLine(string.Format("TRACE [{0}] - {1}", _categoray, _message));
        }
        protected override void debug(string _categoray, string _message)
        {
            Console.WriteLine(string.Format("DEBUG [{0}] - {1}", _categoray, _message));
        }
        protected override void info(string _categoray, string _message)
        {
            Console.WriteLine(string.Format("INFO [{0}] - {1}", _categoray, _message));
        }
        protected override void warning(string _categoray, string _message)
        {
            Console.WriteLine(string.Format("WARNING [{0}] - {1}", _categoray, _message));
        }
        protected override void error(string _categoray, string _message)
        {
            Console.WriteLine(string.Format("ERROR [{0}] - {1}", _categoray, _message));
        }
        protected override void exception(System.Exception _exp)
        {
            Console.WriteLine(string.Format("EXCEPTION [{0}] - {1}", "EXP", _exp.Message));
        }
    }

    class SimpleModel : Model
    {
        public class SimpleStatus : Status
        {
            public string uuid { get; set; }
        }

        private SimpleController? controller { get; set; }

        private SimpleStatus? status
        {
            get
            {
                return status_ as SimpleStatus;
            }
        }

        protected override void preSetup()
        {
            Error err;
            status_ = spawnStatus<SimpleStatus>("SimpleStatus", out err);
            controller = findController("SimpleController") as SimpleController;
        }

        protected override void setup()
        {
            getLogger()?.Info("setup SimpleModel");
        }

        protected override void postDismantle()
        {
            Error err;
            killStatus("SimpleStatus", out err);
        }

        public void UpdateSignin(Model.Status _reply, string _uuid)
        {
            controller!.Signin(_reply, status, _uuid);
        }
    }
    class SimpleView : View
    {
        private SimpleService? service { get; set; }
        protected override void preSetup()
        {
            service = findService("SimpleService") as SimpleService;
        }

        protected override void setup()
        {
            getLogger()?.Info("setup SimpleView");
        }

        public void OnSigninClicked()
        {
            service!.PostSignin("admin", "11223344");
        }

        public void PrintError(string _error)
        {
            getLogger()?.Error(_error);
        }

        public void PrintUUID(SimpleModel.SimpleStatus _status)
        {
            getLogger()?.Info(string.Format("uuid is {0}", _status.uuid));
        }
    }

    class SimpleController : Controller
    {
        private SimpleView? view { get; set; }
        protected override void preSetup()
        {
            view = findView("SimpleView") as SimpleView;
        }
        protected override void setup()
        {
            getLogger()?.Info("setup SimpleController");
        }
        public void Signin(Model.Status _reply, SimpleModel.SimpleStatus _status, string _uuid)
        {
            if (_reply.getCode() != 0)
            {
                view!.PrintError(_reply.getMessage());
                return;
            }
            _status.uuid = _uuid;
            view!.PrintUUID(_status);
        }
    }

    class SimpleService : Service
    {
        private SimpleModel? model { get; set; }
        protected override void preSetup()
        {
            model = findModel("SimpleModel") as SimpleModel;
        }
        protected override void setup()
        {
            getLogger()?.Info("setup SimpleService");
        }

        public void PostSignin(string _username, string _password)
        {
            Dictionary<string, Any> parameter = new Dictionary<string, Any>();
            parameter["username"] = Any.FromString(_username);
            parameter["password"] = Any.FromString(_password);
            /*
            post("http://localhost/signin", parameter, (_reply) =>
            {
                Model.Status reply = Model.Status.New<Model.Status>(0, "");
                string uuid = _reply;
                model.UpdateSignin(reply, uuid);
            }, (_err) =>
            {
                Model.Status reply = Model.Status.New<Model.Status>(_err.getCode(), _err.getMessage());
                model.UpdateSignin(reply, "");
            }, null);
            */
        }
    }

    [TestClass]
    public class FrameworkTests
    {
        [TestMethod]
        public void TestStaticPipe()
        {
            Framework framework = new Framework();
            framework.setLogger(new ConsoleLogger());

            framework.Initialize();
            framework.getStaticPipe().RegisterModel("SimpleModel", new SimpleModel());
            SimpleView simpleView = new SimpleView();
            framework.getStaticPipe().RegisterView("SimpleView", simpleView);
            framework.getStaticPipe().RegisterController("SimpleController", new SimpleController());
            SimpleService service = new SimpleService();
            framework.getStaticPipe().RegisterService("SimpleService", service);

            framework.Setup();

            // ��ѭ��
            // ģ���¼��ť�����ʱ
            {
                simpleView.OnSigninClicked();
            }

            framework.Dismantle();
            framework.getStaticPipe().CancelService("SimpleService");
            framework.getStaticPipe().CancelController("SimpleController");
            framework.getStaticPipe().CancelView("SimpleView");
            framework.getStaticPipe().CancelModel("SimpleModel");

            framework.Release();
        }
    }
}