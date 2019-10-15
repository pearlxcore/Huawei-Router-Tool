@echo off
REM Automatically generated
REM Input: ./huawei_band_tool --network-mode 02 --network-band 2000000000000
@echo on

huawei_band_tool --network-mode 02 --network-band 2000000000000
@echo off
if %errorlevel% neq 0 (EXIT %errorlevel%)
@echo on

