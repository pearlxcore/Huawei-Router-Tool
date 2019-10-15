@echo off
REM Automatically generated
REM Input: ./huawei_band_tool --win32-exit-instantly --network-mode 03 --network-band 3FFFFFFF --lte-band +1800 \ && echo "" && sleep 2 && \ ./huawei_band_tool --network-mode 03 --network-band 3FFFFFFF --lte-band +1800+2100
@echo on

huawei_band_tool --win32-exit-instantly --network-mode 03 --network-band 3FFFFFFF --lte-band +1800
@echo off
if %errorlevel% neq 0 (EXIT %errorlevel%)
@echo on

@echo off
echo[
if %errorlevel% neq 0 (EXIT %errorlevel%)
@echo on

@echo off
TIMEOUT 2 >NUL
if %errorlevel% neq 0 (EXIT %errorlevel%)
@echo on

huawei_band_tool --network-mode 03 --network-band 3FFFFFFF --lte-band +1800+2100
@echo off
if %errorlevel% neq 0 (EXIT %errorlevel%)
@echo on

