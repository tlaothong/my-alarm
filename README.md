# my-alarm
My Alarm is the service/app that do the universal alarm with very easy setup

## Current limitation
1. Current implementation supports only `.wav` file.

## Usage
1. Please setup alarms in the file called `alarms.txt`.
* Run this command to install this app as a Windows service

    ```
    UniAlarm install
    ```

* If you don't want to reboot for the first time usage use this command to start the service immediately

    ```
    UniAlarm start
    ```

* For more information use

    ```
    UniAlarm --help
    ```