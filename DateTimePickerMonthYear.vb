Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Namespace ClassLibrary1
    <DesignerCategory("Code")>
    Public Class CustomControl1 : Inherits DateTimePicker

        Public Sub New()
			InitializeComponent()
            CustomFormat = "yyyy MMM"
            Format = DateTimePickerFormat.Custom
            Value = DateTime.Now
        End Sub

        'override Format to redefine default value (used by designer)
        <DefaultValue(DateTimePickerFormat.Custom)>
        Public Overloads Property Format() As DateTimePickerFormat
            Get
                Return MyBase.Format
            End Get

            Set(ByVal value As DateTimePickerFormat)
                MyBase.Format = value
            End Set
        End Property

        'override CustomFormat to redefine default value (used by designer)
        <DefaultValue("MMM yyyy")>
        Public Overloads Property CustomFormat() As String
            Get
                Return MyBase.CustomFormat
            End Get

            Set(ByVal value As String)
                MyBase.CustomFormat = value
            End Set
        End Property

        Protected Overrides Sub WndProc(ByRef m As Message)

            If m.Msg = WM_NOFITY Then
                Dim val_nmhdr = CType(Marshal.PtrToStructure(m.LParam, GetType(NMHDR)), NMHDR)
                Select Case val_nmhdr.code
                    'detect pop-up display And switch view to month selection
                    Case -950
                        Dim cal = SendMessage(Handle, DTM_GETMONTHCAL, IntPtr.Zero, IntPtr.Zero)
                        SendMessage(cal, MCM_SETCURRENTVIEW, IntPtr.Zero, CType(1, IntPtr))
                        Exit Select

                    'detect pop-up display And switch view to month selection
                    Case MCN_VIEWCHANGE
                        Dim val_nmviewchange = CType(Marshal.PtrToStructure(m.LParam, GetType(NMVIEWCHANGE)), NMVIEWCHANGE)
                        If val_nmviewchange.dwOldView = 1 AndAlso val_nmviewchange.dwNewView = 0 Then
                            SendMessage(Handle, DTM_CLOSEMONTHCAL, IntPtr.Zero, IntPtr.Zero)
                        End If
                        Exit Select
                End Select
            End If
                MyBase.WndProc(m)
        End Sub

        Private Const WM_NOFITY As Integer = &H4E
        Private Const DTM_CLOSEMONTHCAL As Integer = &H1000 + 13
        Private Const DTM_GETMONTHCAL As Integer = &H1000 + 8
        Private Const MCM_SETCURRENTVIEW As Integer = &H1000 + 32
        Private Const MCN_VIEWCHANGE As Integer = -750

        <DllImport("user32.dll")>
        Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal wMsg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr
        End Function

        <StructLayout(LayoutKind.Sequential)>
        Public Structure NMHDR
            Public hwndFrom As IntPtr
            Public idFrom As IntPtr
            Public code As Integer
        End Structure

        <StructLayout(LayoutKind.Sequential)>
        Public Structure NMVIEWCHANGE
            Public nmhdr As NMHDR
            Public dwOldView As UInt32
            Public dwNewView As UInt32
        End Structure

    End Class
End Namespace