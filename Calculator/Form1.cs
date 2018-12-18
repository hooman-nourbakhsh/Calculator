using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Calculator
{
    /// <summary>
    /// این یک برنامه ماشین حساب معمولی هست که با استفاده از مهندسی نرم افزار سعی شده 
    /// تا بصورت ساده ، بهینه و استفاده از کمترین حجم کد درمقابل بالاترین عملکرد را داشته باشیم.

    /// Source: https://t.me/bytelearn
    /// Creator:HooMaN (https://github.com/sharlobin2)
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// تعریف متغیر های سراسری
        /// x: عدد اول
        /// y: عدد دوم
        /// z: حاصل
        /// op: عملگرها
        /// </summary>
        double x, y, z;
        string op;
        Boolean flag;//استفاده از تکنیک پرچم

        public Form1()
        {
            InitializeComponent();
        }

        private void Numbers(object sender, MouseEventArgs e)
        {
            //تابع ای برای تمام شماره ها
            //مشخص می شود رویداد کلیک برای کدام دکمه فراخوانی شده است sender که توسط
            if (flag == true)
            {
                //شد حافظه را پاک و پرچم را خاموش میکنیم true بعد از اینکه یکی از عملگرها وارد شد و پرچم مقدار 
                txtDisplay.Text = "";
                flag = false;
            }
            //سپس عدد دوم را دریافت می کنیم
            txtDisplay.Text += ((Button)sender).Text;
        }

        private void Operators(object sender, MouseEventArgs e)
        {
            //برای چک کردن اینکه آیا عملگر بار زده شده یا بار دوم
            if (op != null)
            {
                btnEqual_Click(null, null);
            }
            //عدد اول را که در جعبه متن وارد شده است ذخیره می کنیم
            x = Convert.ToDouble(txtDisplay.Text);
            //ذخیره کردن عملگر
            op = ((Button)sender).Text;
            flag = true;// تا مقدار عدد اول به همراه عملگر در حافظه ذخیره شود
        }

        private void btnEqual_Click(object sender, MouseEventArgs e)
        {
            //ذخیره عدد دوم
            y = Convert.ToDouble(txtDisplay.Text);
            //انجام عملیات محاسبه توسط تعیین نوع عملگر انتخاب شده
            switch (op)
            {
                case "+":
                    z = x + y;
                    break;

                case "-":
                    z = x - y;
                    break;

                case "*":
                    z = x * y;
                    break;

                case "/":
                    z = x / y;
                    break;
            }
            //نمایش نتیجه
            txtDisplay.Text = z.ToString();
            op = null;//بعد از اینکه عملیات انجام شد عملگر را خالی میکنیم
        }

        private void txtDisplay_TextChanged(object sender, EventArgs e)
        {
            //چک میکنیم وقتی بروی دکمه دات یکبار فشرده شد دکمه ممیز خاموش شود
            btnDot.Enabled = !txtDisplay.Text.Contains(".");

            //با استفاده از این متد طول متن صفحه نمایش را کنترل می کنیم تا درصورت خالی بودن با خطا مواجه نشویم
            btnDel.Enabled = Convert.ToBoolean(txtDisplay.Text.Length);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            //از اولین کاراکتر تا یکی کمتر از طول متن عمل حذف را انجام می دهیم Substringبا استفاده از متد 
            if (txtDisplay.Text.Length > 0)
            {
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //برای اینکه در هنگام اجرای اولیه برنامه صفحه نمایش خالی می باشد اجازه حذف کردن نداریم
            //را صدا می زنیم txtChanged پس متد نوشته شده در رویداد
            txtDisplay_TextChanged(null, null);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //رخ دادی که برای شی فوکوس دار زمانی که کلیدی از صفحه کلید را فشار دهید اتفاق می افتد
            //رای اینکه تمام دکمه های روی فرم این عمل را انجام دهند برای رخ داد فرم آن را فعال میکنیم
            ///فرم فعال باشد تا کلید هارا ببینید Key Preview تکته باید خصوصیت

            foreach (Button x in panel1.Controls)
            {
                ///برای اینکه وقتی از طریق کیبورد دکمه هارا فشار می دهیم عمل فوکوس و تغییر رنگ انجام دهد از حلقه استفاده میکنیم
                if (x.Text == e.KeyChar.ToString())
                {
                    x.Focus();
                    x.ForeColor = Color.Maroon;
                }
                else
                {
                    x.ForeColor = Color.Black;
                }
            }

            Button temp = new Button();// ساخت شی از دکمه
            temp.Text = e.KeyChar.ToString();

            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                Numbers(temp, null);
            }
            else if (e.KeyChar == '+' || e.KeyChar == '-' || e.KeyChar == '*' || e.KeyChar == '/')
            {
                Operators(temp, null);
            }
            else if (e.KeyChar == '=')
            {
                btnEqual_Click(null, null);
            }
            else if (e.KeyChar == '.' && txtDisplay.Text.Contains(".") == false)//اجازه زدن بیشتر از یک میمز را در کیبورد نمی دهیم
            {
                Numbers(temp, null);
            }
            else if (e.KeyChar == '\b')
            {
                btnDel_Click(null, null);
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //استفاده کنیم این رخ داد را فعال می کنیم Enter,f1-12 ,.... وقتی بخواهیم از دکمه های توسعه یافته مثل 
            if (e.KeyCode == Keys.Enter)
            {
                btnEqual_Click(null, null);
                btnEqual.Focus();
                btnEqual.ForeColor = Color.Red;
            }
        }

        private void btnOnOff_Click(object sender, EventArgs e)
        {
            panel1.Enabled = !panel1.Enabled;
            if (btnOnOff.Text == "On")
            {
                btnOnOff.Text = "Off";
                this.KeyPreview = true;
            }
            else
            {
                btnOnOff.Text = "On";
                this.KeyPreview = false;
            }
        }
    }
}