@echo off
echo 🚀 Organizing Galaxus Integration Project...

REM Create directory structure
echo Creating directories...
mkdir docs 2>nul
mkdir templates 2>nul
mkdir archive 2>nul
mkdir archive\original_notes 2>nul
mkdir src 2>nul
mkdir config 2>nul
mkdir tests 2>nul

REM Move and organize files
echo Organizing files...

REM Move the master guide (assuming it exists as a temp file)
if exist "Galaxus Integration Documentation - Comprehensive Analysis.md" (
    move "Galaxus Integration Documentation - Comprehensive Analysis.md" "INTEGRATION_GUIDE.md"
)

REM Archive original notes
if exist "docs\First day documentation.md" (
    move "docs\First day documentation.md" "archive\original_notes\"
)
if exist "docs\Second day documentation.md" (
    move "docs\Second day documentation.md" "archive\original_notes\"
)

REM Move original documentation folders to archive
if exist "docs\Data Integration" (
    move "docs\Data Integration" "archive\original_notes\"
)
if exist "docs\Order integration" (
    move "docs\Order integration" "archive\original_notes\"
)

REM Move any template files
move *.csv templates\ 2>nul
move *.xlsx templates\ 2>nul
move *template*.* templates\ 2>nul
move *sample*.xml templates\ 2>nul

REM Move images if they exist
if exist "docs\Images" (
    move "docs\Images" "docs\"
)

REM Create README if it doesn't exist
if not exist README.md (
    echo # Galaxus Integration Project > README.md
    echo. >> README.md
    echo Swiss marketplace integration using FTP/SFTP file exchange. >> README.md
    echo. >> README.md
    echo ## Documentation >> README.md
    echo - Main Guide: [INTEGRATION_GUIDE.md](INTEGRATION_GUIDE.md^) >> README.md
    echo - Implementation Notes: [docs/](docs/^) >> README.md
    echo. >> README.md
    echo ## Structure >> README.md
    echo - `/templates` - Galaxus CSV/XML templates >> README.md
    echo - `/src` - Integration code >> README.md
    echo - `/config` - Configuration files >> README.md
    echo - `/archive` - Historical documentation >> README.md
)

REM Create .gitignore if it doesn't exist
if not exist .gitignore (
    echo # Environment > .gitignore
    echo .env >> .gitignore
    echo venv/ >> .gitignore
    echo __pycache__/ >> .gitignore
    echo *.pyc >> .gitignore
    echo. >> .gitignore
    echo # Credentials >> .gitignore
    echo config/credentials.* >> .gitignore
    echo *_credentials.* >> .gitignore
    echo. >> .gitignore
    echo # Test files >> .gitignore
    echo test_*.csv >> .gitignore
    echo test_*.xml >> .gitignore
    echo *.log >> .gitignore
    echo. >> .gitignore
    echo # IDE >> .gitignore
    echo .idea/ >> .gitignore
    echo .vscode/ >> .gitignore
    echo *.swp >> .gitignore
    echo. >> .gitignore
    echo # OS >> .gitignore
    echo .DS_Store >> .gitignore
    echo Thumbs.db >> .gitignore
)

REM Initialize git if needed
if not exist .git (
    echo Initializing Git repository...
    git init
)

REM Add all files to git
echo Adding files to Git...
git add .

REM Commit changes
echo Committing changes...
git commit -m "🎯 Organized Galaxus integration documentation structure" -m "- Created clean folder structure" -m "- Moved master guide to root" -m "- Archived original notes" -m "- Added templates directory" -m "- Set up project structure"

REM Check if remote exists
git remote -v | findstr origin >nul
if errorlevel 1 (
    echo.
    echo ⚠️  No Git remote found!
    echo Please add your GitHub repository:
    echo git remote add origin https://github.com/YOUR_USERNAME/YOUR_REPO.git
    echo.
    echo Then push with:
    echo git push -u origin main
) else (
    echo Pushing to GitHub...
    git push -u origin main || git push -u origin master
    echo ✅ Successfully pushed to GitHub!
)

echo.
echo 📁 Project structure organized!
echo 📝 Don't forget to:
echo    - Add your Galaxus templates to /templates
echo    - Update README.md with your specific details
echo    - Add FTP credentials to environment variables
pause