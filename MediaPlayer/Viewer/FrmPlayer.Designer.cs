namespace MediaPlayer
{
    partial class FrmPlayer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPlayer));
            this.progressBar_playing = new System.Windows.Forms.ProgressBar();
            this.grp_graphics = new System.Windows.Forms.GroupBox();
            this.pc_graph = new System.Windows.Forms.PictureBox();
            this.reproduciendoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.libreriaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_forward = new System.Windows.Forms.Button();
            this.txt_main = new System.Windows.Forms.TextBox();
            this.btn_stop = new System.Windows.Forms.Button();
            this.btn_return = new System.Windows.Forms.Button();
            this.btn_next = new System.Windows.Forms.Button();
            this.txt_secondary = new System.Windows.Forms.TextBox();
            this.btn_play = new System.Windows.Forms.Button();
            this.grp_graphics.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pc_graph)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar_playing
            // 
            this.progressBar_playing.Location = new System.Drawing.Point(59, 577);
            this.progressBar_playing.Name = "progressBar_playing";
            this.progressBar_playing.Size = new System.Drawing.Size(1098, 10);
            this.progressBar_playing.TabIndex = 4;
            // 
            // grp_graphics
            // 
            this.grp_graphics.Controls.Add(this.pc_graph);
            this.grp_graphics.Location = new System.Drawing.Point(12, 41);
            this.grp_graphics.Name = "grp_graphics";
            this.grp_graphics.Size = new System.Drawing.Size(1183, 530);
            this.grp_graphics.TabIndex = 5;
            this.grp_graphics.TabStop = false;
            // 
            // pc_graph
            // 
            this.pc_graph.BackColor = System.Drawing.Color.Black;
            this.pc_graph.Location = new System.Drawing.Point(6, 11);
            this.pc_graph.Name = "pc_graph";
            this.pc_graph.Size = new System.Drawing.Size(1171, 513);
            this.pc_graph.TabIndex = 0;
            this.pc_graph.TabStop = false;
            // 
            // reproduciendoToolStripMenuItem
            // 
            this.reproduciendoToolStripMenuItem.Name = "reproduciendoToolStripMenuItem";
            this.reproduciendoToolStripMenuItem.Size = new System.Drawing.Size(125, 24);
            this.reproduciendoToolStripMenuItem.Text = "Reproduciendo";
            // 
            // libreriaToolStripMenuItem
            // 
            this.libreriaToolStripMenuItem.Name = "libreriaToolStripMenuItem";
            this.libreriaToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.libreriaToolStripMenuItem.Text = "Libreria";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reproduciendoToolStripMenuItem,
            this.libreriaToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1207, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btn_back
            // 
            this.btn_back.BackColor = System.Drawing.Color.Black;
            this.btn_back.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_back.BackgroundImage")));
            this.btn_back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_back.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_back.Location = new System.Drawing.Point(436, 626);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(135, 69);
            this.btn_back.TabIndex = 32;
            this.btn_back.UseVisualStyleBackColor = false;
            // 
            // btn_forward
            // 
            this.btn_forward.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_forward.AutoSize = true;
            this.btn_forward.BackColor = System.Drawing.Color.Black;
            this.btn_forward.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_forward.BackgroundImage")));
            this.btn_forward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_forward.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_forward.Location = new System.Drawing.Point(677, 626);
            this.btn_forward.Name = "btn_forward";
            this.btn_forward.Size = new System.Drawing.Size(135, 69);
            this.btn_forward.TabIndex = 31;
            this.btn_forward.UseVisualStyleBackColor = false;
            this.btn_forward.Click += new System.EventHandler(this.btn_forward_Click);
            // 
            // txt_main
            // 
            this.txt_main.BackColor = System.Drawing.Color.Black;
            this.txt_main.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_main.Enabled = false;
            this.txt_main.Location = new System.Drawing.Point(424, 597);
            this.txt_main.Multiline = true;
            this.txt_main.Name = "txt_main";
            this.txt_main.Size = new System.Drawing.Size(395, 124);
            this.txt_main.TabIndex = 25;
            // 
            // btn_stop
            // 
            this.btn_stop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_stop.AutoSize = true;
            this.btn_stop.BackColor = System.Drawing.Color.Black;
            this.btn_stop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_stop.BackgroundImage")));
            this.btn_stop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_stop.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btn_stop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_stop.Location = new System.Drawing.Point(202, 626);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(94, 69);
            this.btn_stop.TabIndex = 29;
            this.btn_stop.UseVisualStyleBackColor = false;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // btn_return
            // 
            this.btn_return.BackColor = System.Drawing.Color.Black;
            this.btn_return.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_return.BackgroundImage")));
            this.btn_return.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_return.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_return.Location = new System.Drawing.Point(283, 626);
            this.btn_return.Name = "btn_return";
            this.btn_return.Size = new System.Drawing.Size(135, 69);
            this.btn_return.TabIndex = 28;
            this.btn_return.UseVisualStyleBackColor = false;
            // 
            // btn_next
            // 
            this.btn_next.BackColor = System.Drawing.Color.Black;
            this.btn_next.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_next.BackgroundImage")));
            this.btn_next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_next.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_next.Location = new System.Drawing.Point(806, 626);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(135, 69);
            this.btn_next.TabIndex = 27;
            this.btn_next.UseVisualStyleBackColor = false;
            // 
            // txt_secondary
            // 
            this.txt_secondary.BackColor = System.Drawing.Color.Black;
            this.txt_secondary.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_secondary.Enabled = false;
            this.txt_secondary.Location = new System.Drawing.Point(163, 612);
            this.txt_secondary.Multiline = true;
            this.txt_secondary.Name = "txt_secondary";
            this.txt_secondary.Size = new System.Drawing.Size(867, 95);
            this.txt_secondary.TabIndex = 26;
            // 
            // btn_play
            // 
            this.btn_play.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_play.AutoSize = true;
            this.btn_play.BackColor = System.Drawing.Color.Black;
            this.btn_play.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_play.BackgroundImage")));
            this.btn_play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_play.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.btn_play.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_play.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_play.Location = new System.Drawing.Point(577, 626);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(94, 69);
            this.btn_play.TabIndex = 31;
            this.btn_play.UseVisualStyleBackColor = false;
            this.btn_play.Click += new System.EventHandler(this.btn_play_Click);
            // 
            // FrmPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1207, 722);
            this.Controls.Add(this.btn_play);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_forward);
            this.Controls.Add(this.txt_main);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_return);
            this.Controls.Add(this.btn_next);
            this.Controls.Add(this.txt_secondary);
            this.Controls.Add(this.grp_graphics);
            this.Controls.Add(this.progressBar_playing);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FrmPlayer";
            this.Text = "Reproductor";
            this.grp_graphics.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pc_graph)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar_playing;
        private System.Windows.Forms.GroupBox grp_graphics;
        private System.Windows.Forms.PictureBox pc_graph;
        private System.Windows.Forms.ToolStripMenuItem reproduciendoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem libreriaToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_forward;
        private System.Windows.Forms.TextBox txt_main;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Button btn_return;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.TextBox txt_secondary;
        private System.Windows.Forms.Button btn_play;
    }
}