using XTC.FMP.LIB.MVCS;

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
        public SimpleModel(string _uid) : base(_uid)
        {
        }
        public class SimpleStatus : Status
        {
            public string uuid { get; set; }
        }

        private SimpleController? controller
        {
            get
            {
                if (null == controller_)
                    controller_ = findController("SimpleController") as SimpleController;
                return controller_;
            }
        }
        private SimpleController? controller_ { get; set; }

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
        public Signal signalEcho;
        public SimpleView(string _uid) : base(_uid)
        {
        }

        private SimpleService? service
        {
            get
            {
                if (null == service_)
                    service_ = findService("SimpleService") as SimpleService;
                return service_;
            }
        }

        private SimpleModel? model
        {
            get
            {
                if (null == model_)
                {
                    model_ = findModel("SimpleModel") as SimpleModel;
                    signalEcho = new Signal(model_);
                    signalEcho.Connect((_status, _data) =>
                    {
                        getLogger().Info($"receive signal: {_data}");
                    });
                }
                return model_;
            }
        }

        private SimpleService? service_ { get; set; }
        private SimpleModel? model_ { get; set; }
        protected override void preSetup()
        {
            addSubscriber("/simple", (_status, _data) =>
            {
                getLogger()?.Info("receive message /simple");

            });
        }

        protected override void setup()
        {
            getLogger()?.Info("setup SimpleView");
            
        }

        public void OnSigninClicked()
        {
            if (null == model)
                return;
            service!.PostSignin("admin", "11223344");
            signalEcho.Emit("~~~~~~");
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
        public SimpleController(string _uid) : base(_uid)
        {
        }

        private SimpleView? view
        {
            get
            {
                if (null == view_)
                    view_ = findView("SimpleView") as SimpleView;
                return view_;
            }
        }
        private SimpleView? view_ { get; set; }
        protected override void preSetup()
        {
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
        public SimpleService(string _uid) : base(_uid)
        {
        }
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
            model?.Publish("/simple", "");
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
        private SimpleModel? model;
        private SimpleView? view;
        private SimpleController? controller;
        private SimpleService? service;

        [TestMethod]
        public void TestStaticPipe()
        {
            Framework framework = new Framework();
            framework.setLogger(new ConsoleLogger());

            framework.Initialize();
            model = new SimpleModel("SimpleModel");
            framework.getStaticPipe().RegisterModel(model);
            view = new SimpleView("SimpleModel");
            framework.getStaticPipe().RegisterView(view);
            controller = new SimpleController("SimpleController");
            framework.getStaticPipe().RegisterController(controller);
            service = new SimpleService("SimpleService");
            framework.getStaticPipe().RegisterService(service);

            framework.Setup();

            // 主循环
            // 模拟登录按钮被点击时
            {
                view.OnSigninClicked();
            }

            framework.Dismantle();
            framework.getStaticPipe().CancelService(service);
            framework.getStaticPipe().CancelController(controller);
            framework.getStaticPipe().CancelView(view);
            framework.getStaticPipe().CancelModel(model);

            framework.Release();
        }

        [TestMethod]
        public void TestDynamicPipe()
        {
            Framework framework = new Framework();
            framework.setLogger(new ConsoleLogger());

            framework.Initialize();
            framework.Setup();

            model = new SimpleModel("SimpleModel");
            framework.getDynamicPipe().PushModel(model);
            view = new SimpleView("SimpleModel");
            framework.getDynamicPipe().PushView(view);
            controller = new SimpleController("SimpleController");
            framework.getDynamicPipe().PushController(controller);
            service = new SimpleService("SimpleService");
            framework.getDynamicPipe().PushService(service);

            // 主循环
            // 模拟登录按钮被点击时
            {
                view.OnSigninClicked();
            }

            framework.getDynamicPipe().PopService(service);
            framework.getDynamicPipe().PopController(controller);
            framework.getDynamicPipe().PopView(view);
            framework.getDynamicPipe().PopModel(model);

            framework.Dismantle();
            framework.Release();
        }
    }
}