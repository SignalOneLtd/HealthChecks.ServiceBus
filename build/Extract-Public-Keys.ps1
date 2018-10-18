$sn = "C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\sn.exe"

gci -Path "*.snk" | % {
    & $sn -p $_.Name ("{0}.pk" -f $_.Name)
    & $sn -tp ("{0}.pk" -f $_.Name)
}