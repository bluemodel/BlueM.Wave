Namespace My

    ' F�r MyApplication sind folgende Ereignisse verf�gbar:
    ' 
    ' Startup: Wird beim Starten der Anwendung noch vor dem Erstellen des Startformulars ausgel�st.
    ' Shutdown: Wird nach dem Schlie�en aller Anwendungsformulare ausgel�st. Dieses Ereignis wird nicht ausgel�st, wenn die Anwendung nicht normal beendet wird.
    ' UnhandledException: Wird ausgel�st, wenn in der Anwendung eine unbehandelte Ausnahme auftritt.
    ' StartupNextInstance: Wird beim Starten einer Einzelinstanzanwendung ausgel�st, wenn diese bereits aktiv ist. 
    ' NetworkAvailabilityChanged: Wird beim Herstellen oder Trennen der Netzwerkverbindung ausgel�st.
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup

            '�bergabeparameter verarbeiten
            For Each param As String In e.CommandLine

                'Dateien �ffnen
                If (System.IO.File.Exists(param)) Then
                    Call Wave.Import_File(param)
                End If

            Next
        End Sub

    End Class

End Namespace

