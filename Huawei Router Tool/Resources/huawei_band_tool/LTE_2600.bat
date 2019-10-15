@echo off
REM Automatically generated
REM Input: ./huawei_band_tool --network-mode 03 --network-band 3FFFFFFF --lte-band +2600
@echo on

huawei_band_tool --network-mode 03 --network-band 3FFFFFFF --lte-band +2600
@echo off
if %errorlevel% neq 0 (EXIT %errorlevel%)
@echo on

