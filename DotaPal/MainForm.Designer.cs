namespace DotaPal
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.cooldownTimer = new System.Timers.Timer();
            this.groupAdd = new System.Windows.Forms.GroupBox();
            this.labelAddExtra2 = new System.Windows.Forms.Label();
            this.labelAddExtra1 = new System.Windows.Forms.Label();
            this.labelAddHero5 = new System.Windows.Forms.Label();
            this.labelAddHero4 = new System.Windows.Forms.Label();
            this.labelAddHero3 = new System.Windows.Forms.Label();
            this.labelAddHero2 = new System.Windows.Forms.Label();
            this.labelAddHero1 = new System.Windows.Forms.Label();
            this.groupReset = new System.Windows.Forms.GroupBox();
            this.labelResetExtra2 = new System.Windows.Forms.Label();
            this.labelResetExtra1 = new System.Windows.Forms.Label();
            this.labelResetHero5 = new System.Windows.Forms.Label();
            this.labelResetHero4 = new System.Windows.Forms.Label();
            this.labelResetHero3 = new System.Windows.Forms.Label();
            this.labelResetHero2 = new System.Windows.Forms.Label();
            this.labelResetHero1 = new System.Windows.Forms.Label();
            this.groupChangeSide = new System.Windows.Forms.GroupBox();
            this.labelChangeSide = new System.Windows.Forms.Label();
            this.btnLangEn = new System.Windows.Forms.Button();
            this.btnLangRu = new System.Windows.Forms.Button();
            this.groupToggle = new System.Windows.Forms.GroupBox();
            this.labelToggleOverlay = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize) (this.cooldownTimer)).BeginInit();
            this.groupAdd.SuspendLayout();
            this.groupReset.SuspendLayout();
            this.groupChangeSide.SuspendLayout();
            this.groupToggle.SuspendLayout();
            this.SuspendLayout();
            // 
            // cooldownTimer
            // 
            this.cooldownTimer.Enabled = true;
            this.cooldownTimer.Interval = 1000D;
            this.cooldownTimer.SynchronizingObject = this;
            this.cooldownTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.cooldownTimer_Elapsed);
            // 
            // groupAdd
            // 
            this.groupAdd.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupAdd.Controls.Add(this.labelAddExtra2);
            this.groupAdd.Controls.Add(this.labelAddExtra1);
            this.groupAdd.Controls.Add(this.labelAddHero5);
            this.groupAdd.Controls.Add(this.labelAddHero4);
            this.groupAdd.Controls.Add(this.labelAddHero3);
            this.groupAdd.Controls.Add(this.labelAddHero2);
            this.groupAdd.Controls.Add(this.labelAddHero1);
            this.groupAdd.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            resources.ApplyResources(this.groupAdd, "groupAdd");
            this.groupAdd.Name = "groupAdd";
            this.groupAdd.TabStop = false;
            // 
            // labelAddExtra2
            // 
            resources.ApplyResources(this.labelAddExtra2, "labelAddExtra2");
            this.labelAddExtra2.Name = "labelAddExtra2";
            // 
            // labelAddExtra1
            // 
            resources.ApplyResources(this.labelAddExtra1, "labelAddExtra1");
            this.labelAddExtra1.Name = "labelAddExtra1";
            // 
            // labelAddHero5
            // 
            resources.ApplyResources(this.labelAddHero5, "labelAddHero5");
            this.labelAddHero5.Name = "labelAddHero5";
            // 
            // labelAddHero4
            // 
            resources.ApplyResources(this.labelAddHero4, "labelAddHero4");
            this.labelAddHero4.Name = "labelAddHero4";
            // 
            // labelAddHero3
            // 
            this.labelAddHero3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            resources.ApplyResources(this.labelAddHero3, "labelAddHero3");
            this.labelAddHero3.Name = "labelAddHero3";
            // 
            // labelAddHero2
            // 
            resources.ApplyResources(this.labelAddHero2, "labelAddHero2");
            this.labelAddHero2.Name = "labelAddHero2";
            // 
            // labelAddHero1
            // 
            resources.ApplyResources(this.labelAddHero1, "labelAddHero1");
            this.labelAddHero1.Name = "labelAddHero1";
            // 
            // groupReset
            // 
            this.groupReset.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupReset.Controls.Add(this.labelResetExtra2);
            this.groupReset.Controls.Add(this.labelResetExtra1);
            this.groupReset.Controls.Add(this.labelResetHero5);
            this.groupReset.Controls.Add(this.labelResetHero4);
            this.groupReset.Controls.Add(this.labelResetHero3);
            this.groupReset.Controls.Add(this.labelResetHero2);
            this.groupReset.Controls.Add(this.labelResetHero1);
            this.groupReset.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            resources.ApplyResources(this.groupReset, "groupReset");
            this.groupReset.Name = "groupReset";
            this.groupReset.TabStop = false;
            // 
            // labelResetExtra2
            // 
            resources.ApplyResources(this.labelResetExtra2, "labelResetExtra2");
            this.labelResetExtra2.Name = "labelResetExtra2";
            // 
            // labelResetExtra1
            // 
            resources.ApplyResources(this.labelResetExtra1, "labelResetExtra1");
            this.labelResetExtra1.Name = "labelResetExtra1";
            // 
            // labelResetHero5
            // 
            resources.ApplyResources(this.labelResetHero5, "labelResetHero5");
            this.labelResetHero5.Name = "labelResetHero5";
            // 
            // labelResetHero4
            // 
            resources.ApplyResources(this.labelResetHero4, "labelResetHero4");
            this.labelResetHero4.Name = "labelResetHero4";
            // 
            // labelResetHero3
            // 
            resources.ApplyResources(this.labelResetHero3, "labelResetHero3");
            this.labelResetHero3.Name = "labelResetHero3";
            // 
            // labelResetHero2
            // 
            resources.ApplyResources(this.labelResetHero2, "labelResetHero2");
            this.labelResetHero2.Name = "labelResetHero2";
            // 
            // labelResetHero1
            // 
            resources.ApplyResources(this.labelResetHero1, "labelResetHero1");
            this.labelResetHero1.Name = "labelResetHero1";
            // 
            // groupChangeSide
            // 
            this.groupChangeSide.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupChangeSide.Controls.Add(this.labelChangeSide);
            resources.ApplyResources(this.groupChangeSide, "groupChangeSide");
            this.groupChangeSide.Name = "groupChangeSide";
            this.groupChangeSide.TabStop = false;
            // 
            // labelChangeSide
            // 
            resources.ApplyResources(this.labelChangeSide, "labelChangeSide");
            this.labelChangeSide.Name = "labelChangeSide";
            // 
            // btnLangEn
            // 
            this.btnLangEn.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnLangEn, "btnLangEn");
            this.btnLangEn.Name = "btnLangEn";
            this.btnLangEn.UseVisualStyleBackColor = false;
            this.btnLangEn.Click += new System.EventHandler(this.btnLangEn_Click);
            // 
            // btnLangRu
            // 
            resources.ApplyResources(this.btnLangRu, "btnLangRu");
            this.btnLangRu.Name = "btnLangRu";
            this.btnLangRu.UseVisualStyleBackColor = true;
            this.btnLangRu.Click += new System.EventHandler(this.btnLangRu_Click);
            // 
            // groupToggle
            // 
            this.groupToggle.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupToggle.Controls.Add(this.labelToggleOverlay);
            resources.ApplyResources(this.groupToggle, "groupToggle");
            this.groupToggle.Name = "groupToggle";
            this.groupToggle.TabStop = false;
            // 
            // labelToggleOverlay
            // 
            resources.ApplyResources(this.labelToggleOverlay, "labelToggleOverlay");
            this.labelToggleOverlay.Name = "labelToggleOverlay";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.groupToggle);
            this.Controls.Add(this.btnLangRu);
            this.Controls.Add(this.btnLangEn);
            this.Controls.Add(this.groupChangeSide);
            this.Controls.Add(this.groupReset);
            this.Controls.Add(this.groupAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize) (this.cooldownTimer)).EndInit();
            this.groupAdd.ResumeLayout(false);
            this.groupReset.ResumeLayout(false);
            this.groupChangeSide.ResumeLayout(false);
            this.groupToggle.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.GroupBox groupToggle;
        private System.Windows.Forms.Label labelToggleOverlay;

        private System.Windows.Forms.Button btnLangRu;

        private System.Windows.Forms.Button btnLangEn;

        private System.Windows.Forms.Label labelAddExtra2;

        private System.Windows.Forms.Label labelChangeSide;

        private System.Windows.Forms.GroupBox groupChangeSide;

        private System.Windows.Forms.GroupBox groupReset;
        private System.Windows.Forms.Label labelResetHero3;
        private System.Windows.Forms.Label labelAddHero5;
        private System.Windows.Forms.Label labelAddExtra1;
        private System.Windows.Forms.Label labelResetHero2;
        private System.Windows.Forms.Label labelResetHero1;
        private System.Windows.Forms.Label labelResetExtra2;
        private System.Windows.Forms.Label labelResetExtra1;
        private System.Windows.Forms.Label labelResetHero5;
        private System.Windows.Forms.Label labelResetHero4;

        private System.Windows.Forms.Label labelAddHero2;
        private System.Windows.Forms.Label labelAddHero3;
        private System.Windows.Forms.Label labelAddHero4;

        private System.Windows.Forms.GroupBox groupAdd;
        private System.Windows.Forms.Label labelAddHero1;

        private System.Timers.Timer cooldownTimer;

        #endregion
    }
}