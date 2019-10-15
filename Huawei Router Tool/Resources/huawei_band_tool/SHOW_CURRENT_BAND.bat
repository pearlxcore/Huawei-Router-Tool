@echo off
REM Automatically generated
REM Input: ./huawei_band_tool --show-band
@echo on

huawei_band_tool --show-band
@echo off
if %errorlevel% neq 0 (EXIT %errorlevel%)
@echo on

