using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Okkula;

namespace OkkulaTest {
    public partial class MainWindow : Window {

        public MainWindow() {
            this.Initialize();
        }

        public override void OnLoad(object sender, EventArgs e) {
            this.Title = "Okula Application";
        }

        public override void OnUpdate(object shader, EventArgs e) {

        }
    }
}
