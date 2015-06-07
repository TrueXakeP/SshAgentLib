//
// PassphraseDialog.cs
//
// Author(s): David Lechner <david@lechnology.com>
//
// Copyright (c) 2013 David Lechner
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Security;
using QtGui;
using System.Text;
using QtCore;

namespace dlech.SshAgentLib.Ui.QtAgent
{
  public partial class PassphraseDialog : QDialog
  {
    private SecureEdit mSecureEdit;

    public PassphraseDialog()
    {
      SetupUi(this);
      Message = string.Empty;

      mSecureEdit = new SecureEdit();

      ShowEvent += PassphraseDialog_ShowEvent;
      HideEvent += PassphraseDialog_HideEvent;
    }

    public string Message {
      get {
        return mMessageLabel.Text;
      }
      set {
        mMessageLabel.Text = value;
        mMessageLabel.Visible = !string.IsNullOrWhiteSpace(value);
      }
    }

    public byte[] GetPassphrase()
    {
      return mSecureEdit.ToUtf8();
    }

//    public SecureString GetSecurePassphrase()
//    {
//      return mSecureEdit.SecureString;
//    }

    [Q_SLOT]
    private void PassphraseDialog_ShowEvent(object aSender,
                                            QEventArgs<QShowEvent> aEventArgs)
    {
      mSecureEdit.Attach(mPassphraseLineEdit, true);
      mPassphraseLineEdit.FocusWidget();
    }

    [Q_SLOT]
    private void PassphraseDialog_HideEvent(object aSender,
                                            QEventArgs<QHideEvent> aEventArgs)
    {
      mSecureEdit.Detach();
    }
  }
}