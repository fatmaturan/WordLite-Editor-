using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace miniword
{
    public partial class Form1 : Form
    {
        // Form1 kurucu metodu
        public Form1()
        {
            InitializeComponent();
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Bold); // Kalın stilini değiştirir
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Italic); // İtalik stilini değiştirir
        }

       
        private void button3_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Underline); // Altı çizili stilini değiştirir
        }

       
        private void button4_Click(object sender, EventArgs e)
        {
            ToggleFontStyle(FontStyle.Strikeout); // Üstü çizili stilini değiştirir
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Renk seçim diyalog penceresi oluşturulur
            using (ColorDialog colorDialog = new ColorDialog())
            {
                // Kullanıcı bir renk seçerse
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    // Seçili metnin rengi değiştir
                    richTextBox1.SelectionColor = colorDialog.Color;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Seçili metni sağa hizalar
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        
        private void button7_Click(object sender, EventArgs e)
        {
            // Seçili metni sola hizalar
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Seçili metni ortalar
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        // Yazı stilini değiştirme işlemini yapan metot
        private void ToggleFontStyle(FontStyle style)
        {
            // Eğer seçili metin mevcutsa
            if (richTextBox1.SelectionFont != null)
            {
                // Mevcut yazı tipini al
                Font currentFont = richTextBox1.SelectionFont;
                // Yeni yazı stilini belirle (mevcut stile göre değiştir)
                FontStyle newFontStyle = currentFont.Style ^ style;
                // Yeni yazı tipini uygula
                richTextBox1.SelectionFont = new Font(currentFont, newFontStyle);
            }
        }

       
        private void button9_Click(object sender, EventArgs e)
        {
            // Eğer seçili metin varsa
            if (richTextBox1.SelectionLength > 0)
            {
                // Seçili metni panoya kopyala
                Clipboard.SetText(richTextBox1.SelectedText);
            }
        }

       
        private void button10_Click(object sender, EventArgs e)
        {
            // Eğer panoda metin varsa
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                // Panodaki düz metni al
                string plainText = Clipboard.GetText(TextDataFormat.Text);

                // Mevcut seçim yerine yapıştır
                int selectionStart = richTextBox1.SelectionStart;
                richTextBox1.SelectedText = plainText;

                // İmleci yapıştırılan metnin sonuna taşı
                richTextBox1.SelectionStart = selectionStart + plainText.Length;
            }
        }

     
        private void button11_Click(object sender, EventArgs e)
        {
            SaveFile(); // Dosya kaydetme işlemini çağır
        }

        // Dosyayı açan butonun tıklama olayı
        private void button12_Click(object sender, EventArgs e)
        {
            OpenFile(); // Dosya açma işlemini çağır
        }

        // Dosya açma işlemini yapan metot
        private void OpenFile()
        {
            // Dosya açma diyalog penceresi oluştur
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files|*.txt|All Files|*.*"; // Dosya filtrelerini belirle

            // Kullanıcı bir dosya seçerse
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Dosya yolunu al
                string filePath = openFileDialog.FileName;
                // Dosya içeriğini oku
                string fileContent = File.ReadAllText(filePath);
                // RichTextBox içine dosya içeriğini yaz
                richTextBox1.Text = fileContent;
            }
        }

        // Dosya kaydetme işlemini yapan metot
        private void SaveFile()
        {
            // Dosya kaydetme diyalog penceresi oluştur
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files|*.txt|All Files|*.*"; // Dosya filtrelerini belirle

            // Kullanıcı bir dosya adı belirtirse
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Dosya yolunu al
                string filePath = saveFileDialog.FileName;
                // Dosya içeriğini yaz
                File.WriteAllText(filePath, richTextBox1.Text);
            }
        }

        
        private void button13_Click(object sender, EventArgs e)
        {
            // Yazı tipi seçim diyalog penceresi oluştur
            FontDialog fontDialog = new FontDialog();
            // Kullanıcı bir yazı tipi seçerse
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                // Seçili metne yeni yazı tipini uygula
                richTextBox1.SelectionFont = fontDialog.Font;
            }
        }

      
        private void button14_Click(object sender, EventArgs e)
        {
            // Eğer seçili metin varsa
            if (richTextBox1.SelectedText != string.Empty)
            {
                // Seçili metni panoya kopyala
                Clipboard.SetText(richTextBox1.SelectedText);
                // Seçili metni RichTextBox'tan sil
                richTextBox1.SelectedText = string.Empty;
            }
        }

        
        private void button15_Click(object sender, EventArgs e)
        {
            // TextBox'ta belirtilen dosya adını al
            string dosyaAdi = textBox1.Text.Trim();
            // Dosya yolunu belirle
            string dosyaYolu = @"C:\Users\fatma\FatmaTuran\" + dosyaAdi + ".rtf";

            // Dosya zaten var mı kontrol et
            if (File.Exists(dosyaYolu))
            {
                // Dosya zaten var uyarısı göster
                MessageBox.Show("Dosya zaten var ", "Uyarı", MessageBoxButtons.OK);
            }
            else
            {
                try
                {
                    // Dosyayı kaydet
                    richTextBox1.SaveFile(dosyaYolu);
                    // Başarı mesajı göster
                    MessageBox.Show("Dosya oluşturuldu", "Bilgi", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    // Hata mesajı göster
                    MessageBox.Show("Dosya oluşturulamadı: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

      
        private void button16_Click(object sender, EventArgs e)
        {
            // TextBox'ta belirtilen dosya adını al
            string dosyaAdi = textBox2.Text.Trim();
            // Dosya yolunu belirle
            string dosyaYolu = @"C:\Users\fatma\FatmaTuran\" + dosyaAdi + ".rtf";

            // Dosya mevcut mu kontrol et
            if (File.Exists(dosyaYolu))
            {
                try
                {
                    // Dosya varsa, varsayılan ilişkili uygulamayla aç
                    Process.Start(dosyaYolu);
                }
                catch (Exception ex)
                {
                    // Hata mesajı göster
                    MessageBox.Show("Dosya açılamadı: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Dosya bulunamadı uyarısı göster
                MessageBox.Show("Dosya bulunamadı", "Uyarı", MessageBoxButtons.OK);
            }
        }
    }
}

