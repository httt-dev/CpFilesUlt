﻿namespace CpFiles
{
    partial class SelectArea
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
            this.panelDrag = new System.Windows.Forms.Panel();
            this.btnCaptureThis = new System.Windows.Forms.Button();
            this.panelDrag.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDrag
            // 
            this.panelDrag.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDrag.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelDrag.Controls.Add(this.btnCaptureThis);
            this.panelDrag.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.panelDrag.Location = new System.Drawing.Point(24, 23);
            this.panelDrag.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panelDrag.Name = "panelDrag";
            this.panelDrag.Size = new System.Drawing.Size(1032, 581);
            this.panelDrag.TabIndex = 0;
            this.panelDrag.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelDrag_MouseDown);
            // 
            // btnCaptureThis
            // 
            this.btnCaptureThis.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCaptureThis.Location = new System.Drawing.Point(8, 8);
            this.btnCaptureThis.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCaptureThis.Name = "btnCaptureThis";
            this.btnCaptureThis.Size = new System.Drawing.Size(100, 52);
            this.btnCaptureThis.TabIndex = 0;
            this.btnCaptureThis.Text = "Cap";
            this.btnCaptureThis.UseVisualStyleBackColor = true;
            this.btnCaptureThis.Click += new System.EventHandler(this.btnCaptureThis_Click);
            // 
            // SelectArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 627);
            this.Controls.Add(this.panelDrag);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "SelectArea";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Area";
            this.panelDrag.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelDrag;
        private System.Windows.Forms.Button btnCaptureThis;
    }
}