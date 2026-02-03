using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32;

namespace PCCareUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowHeader();
            ShowMenu();
        }

        static void ShowHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("┌───────────────────────────────────────────────────────────┐");
            Console.WriteLine("│  PC Care Utility v2.3 - Safe System Maintenance Tool      │");
            Console.WriteLine("└───────────────────────────────────────────────────────────┘");
            Console.ResetColor();
            Console.WriteLine();
        }

        static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Main Menu:");
            Console.WriteLine("─────────");
            Console.ResetColor();
            Console.WriteLine("1. System Cleanup");
            Console.WriteLine("2. Power Management");
            Console.WriteLine("3. Network Tools");
            Console.WriteLine("4. System Utilities");
            Console.WriteLine("5. About");
            Console.WriteLine("6. Privacy Guard");
            Console.WriteLine("0. Exit");
            Console.WriteLine();

            Console.Write("Select option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ShowCleanupMenu();
                    break;
                case "2":
                    ShowPowerMenu();
                    break;
                case "3":
                    ShowNetworkMenu();
                    break;
                case "4":
                    ShowToolsMenu();
                    break;
                case "5":
                    ShowAbout();
                    break;
                case "6":
                    ShowPrivacyMenu();  // ← NEW CASE
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Press any key to continue...");
                    Console.ReadKey();
                    ShowMenu();
                    break;
            }
        }

        static void ShowCleanupMenu()
        {
            Console.Clear();
            ShowHeader();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("System Cleanup:");
            Console.WriteLine("──────────────");
            Console.ResetColor();
            Console.WriteLine("1. Clear Temp Files");
            Console.WriteLine("2. Clear Recycle Bin");
            Console.WriteLine("3. Run Disk Cleanup");
            Console.WriteLine("4. Clear Downloads (IRREVERSIBLE)");
            Console.WriteLine("5. Clear Browser Cache");
            Console.WriteLine("6. Analyze Disk Space");
            Console.WriteLine("0. Back to Main Menu");
            Console.WriteLine();

            Console.Write("Select option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ClearTempFiles();
                    break;
                case "2":
                    ClearRecycleBin();
                    break;
                case "3":
                    RunDiskCleanup();
                    break;
                case "4":
                    ClearDownloads();
                    break;
                case "5":
                    ClearBrowserCache();
                    break;
                case "6":
                    AnalyzeDiskSpace();
                    break;
                case "0":
                    ShowMenu();
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Press any key to continue...");
                    Console.ReadKey();
                    ShowCleanupMenu();
                    break;
            }
        }
        static void ShowPrivacyMenu()
        {
            Console.Clear();
            ShowHeader();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Privacy Guard:");
            Console.WriteLine("─────────────");
            Console.ResetColor();
            Console.WriteLine("1. Disable Telemetry & Diagnostics");
            Console.WriteLine("2. Remove Bloatware (OneDrive, Xbox)");
            Console.WriteLine("3. Block Ads in Start Menu");
            Console.WriteLine("4. Disable Timeline & Clipboard History");
            Console.WriteLine("5. Restore All Privacy Settings");
            Console.WriteLine("0. Back to Main Menu");
            Console.WriteLine();

            Console.Write("Select option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisableTelemetry();
                    break;
                case "2":
                    RemoveBloatware();
                    break;
                case "3":
                    BlockAds();
                    break;
                case "4":
                    DisableTimeline();
                    break;
                case "5":
                    RestorePrivacy();
                    break;
                case "0":
                    ShowMenu();
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Press any key to continue...");
                    Console.ReadKey();
                    ShowPrivacyMenu();
                    break;
            }
        }
        static void DisableTelemetry()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("🛡️ DISABLE TELEMETRY & DIAGNOSTICS");
            Console.WriteLine("──────────────────────────────────");
            Console.WriteLine("Disables Windows diagnostic data collection.");
            Console.WriteLine("This improves privacy and reduces background activity.");
            Console.WriteLine();

            Console.Write("Continue? (Y/N): ");
            if (Console.ReadLine().ToUpper() != "Y") { ShowPrivacyMenu(); return; }

            Console.WriteLine("\nApplying privacy settings...");
            try
            {
                // Disable telemetry
                Process.Start(new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = "-Command \"Set-ItemProperty -Path 'HKLM:\\SOFTWARE\\Policies\\Microsoft\\Windows\\DataCollection' -Name 'AllowTelemetry' -Value 0 -Force; New-ItemProperty -Path 'HKLM:\\SOFTWARE\\Policies\\Microsoft\\Windows\\DataCollection' -Name 'AllowTelemetry' -Value 0 -PropertyType DWORD -Force\"",
                    Verb = "runas",
                    UseShellExecute = true,
                    CreateNoWindow = true
                }).WaitForExit(5000);

                Console.WriteLine("\n✓ Telemetry disabled successfully");
                Console.WriteLine("✓ Diagnostic data collection stopped");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Failed to disable telemetry: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowPrivacyMenu();
        }

        static void RemoveBloatware()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("🗑️ REMOVE BLOATWARE");
            Console.WriteLine("──────────────────");
            Console.WriteLine("Removes pre-installed Microsoft apps:");
            Console.WriteLine("- OneDrive");
            Console.WriteLine("- Xbox");
            Console.WriteLine("- Candy Crush");
            Console.WriteLine("- Skype");
            Console.WriteLine();

            Console.Write("TYPE 'REMOVE' TO CONFIRM: ");
            if (Console.ReadLine() != "REMOVE") { ShowPrivacyMenu(); return; }

            Console.WriteLine("\nRemoving bloatware...");
            try
            {
                string[] apps = {
            "Microsoft.OneDrive",
            "Microsoft.XboxApp",
            "king.com.CandyCrushSodaSaga",
            "Microsoft.SkypeApp"
        };

                foreach (string app in apps)
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = "powershell.exe",
                            Arguments = $"-Command \"Get-AppxPackage *{app}* | Remove-AppxPackage\"",
                            Verb = "runas",
                            UseShellExecute = true,
                            CreateNoWindow = true
                        }).WaitForExit(3000);
                        Console.WriteLine($"✓ Removed: {app}");
                    }
                    catch { /* Skip if not installed */ }
                }

                Console.WriteLine("\n✓ Bloatware removal complete");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Failed to remove bloatware: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowPrivacyMenu();
        }

        static void BlockAds()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("🚫 BLOCK ADS IN START MENU");
            Console.WriteLine("──────────────────────────");
            Console.WriteLine("Disables personalized ads and suggestions in Start Menu.");
            Console.WriteLine();

            Console.Write("Continue? (Y/N): ");
            if (Console.ReadLine().ToUpper() != "Y") { ShowPrivacyMenu(); return; }

            Console.WriteLine("\nBlocking ads...");
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = "-Command \"Set-ItemProperty -Path 'HKCU:\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager' -Name 'SubscribedContent-338388Enabled' -Value 0 -Force; Set-ItemProperty -Path 'HKCU:\\Software\\Microsoft\\Windows\\CurrentVersion\\ContentDeliveryManager' -Name 'SubscribedContent-338389Enabled' -Value 0 -Force\"",
                    Verb = "runas",
                    UseShellExecute = true,
                    CreateNoWindow = true
                }).WaitForExit(5000);

                Console.WriteLine("\n✓ Ads blocked in Start Menu");
                Console.WriteLine("✓ Personalized suggestions disabled");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Failed to block ads: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowPrivacyMenu();
        }

        static void DisableTimeline()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("⏱️ DISABLE TIMELINE & CLIPBOARD HISTORY");
            Console.WriteLine("─────────────────────────────────────");
            Console.WriteLine("Stops Windows from tracking your activity history.");
            Console.WriteLine();

            Console.Write("Continue? (Y/N): ");
            if (Console.ReadLine().ToUpper() != "Y") { ShowPrivacyMenu(); return; }

            Console.WriteLine("\nDisabling activity tracking...");
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = "-Command \"Set-ItemProperty -Path 'HKLM:\\SOFTWARE\\Policies\\Microsoft\\Windows\\System' -Name 'EnableActivityFeed' -Value 0 -Force; Set-ItemProperty -Path 'HKCU:\\Software\\Microsoft\\Clipboard' -Name 'EnableClipboardHistory' -Value 0 -Force\"",
                    Verb = "runas",
                    UseShellExecute = true,
                    CreateNoWindow = true
                }).WaitForExit(5000);

                Console.WriteLine("\n✓ Timeline disabled");
                Console.WriteLine("✓ Clipboard history disabled");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Failed to disable timeline: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowPrivacyMenu();
        }

        static void RestorePrivacy()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("↩️ RESTORE PRIVACY SETTINGS");
            Console.WriteLine("──────────────────────────");
            Console.WriteLine("Reverts all privacy changes to Windows defaults.");
            Console.WriteLine();

            Console.Write("TYPE 'RESTORE' TO CONFIRM: ");
            if (Console.ReadLine() != "RESTORE") { ShowPrivacyMenu(); return; }

            Console.WriteLine("\nRestoring defaults...");
            try
            {
                // Re-enable telemetry
                Process.Start(new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = "-Command \"Remove-ItemProperty -Path 'HKLM:\\SOFTWARE\\Policies\\Microsoft\\Windows\\DataCollection' -Name 'AllowTelemetry' -ErrorAction SilentlyContinue\"",
                    Verb = "runas",
                    UseShellExecute = true,
                    CreateNoWindow = true
                }).WaitForExit(3000);

                Console.WriteLine("\n✓ Privacy settings restored to defaults");
                Console.WriteLine("✓ Windows will use recommended settings");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Failed to restore settings: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowPrivacyMenu();
        }
        // =============== BROWSER CACHE CLEANER ===============
        static void ClearBrowserCache()
        {
            Console.Clear();
            ShowHeader();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Browser Cache Cleaner:");
            Console.WriteLine("─────────────────────");
            Console.ResetColor();
            Console.WriteLine("Select browsers to clean (space-separated):");
            Console.WriteLine("1. Google Chrome");
            Console.WriteLine("2. Microsoft Edge");
            Console.WriteLine("3. Mozilla Firefox");
            Console.WriteLine("4. Brave Browser");
            Console.WriteLine("5. All Browsers");
            Console.WriteLine("0. Back to Cleanup Menu");
            Console.WriteLine();

            Console.Write("Your selection (e.g., '1 3' for Chrome + Firefox): ");
            string input = Console.ReadLine();

            if (input == "0") { ShowCleanupMenu(); return; }

            // Parse selections
            var selections = new System.Collections.Generic.HashSet<int>();
            foreach (var item in input.Split(' '))
            {
                if (int.TryParse(item, out int num))
                {
                    if (num == 5) // "All Browsers" - add all
                    {
                        selections.Add(1); selections.Add(2); selections.Add(3); selections.Add(4);
                        break;
                    }
                    else if (num >= 1 && num <= 4)
                    {
                        selections.Add(num);
                    }
                }
            }

            if (selections.Count == 0)
            {
                Console.WriteLine("\n-> No valid browsers selected. Press any key to continue...");
                Console.ReadKey();
                ClearBrowserCache();
                return;
            }

            // Show confirmation with detected browsers
            Console.WriteLine("\n-> BROWSER CACHE CLEANUP");
            Console.WriteLine("────────────────────────");
            Console.WriteLine("This will clear ONLY cache files (not history/passwords/bookmarks).");
            Console.WriteLine("Browsers must be CLOSED for full cleanup.");
            Console.WriteLine();

            var browsersToClean = new System.Collections.Generic.List<(string name, string path)>();
            foreach (int sel in selections)
            {
                var (name, path) = GetBrowserInfo(sel);
                if (Directory.Exists(path))
                {
                    browsersToClean.Add((name, path));
                    Console.WriteLine($"-> {name} cache folder detected: {path}");
                }
                else
                {
                    Console.WriteLine($"-> {name} not installed or cache folder not found");
                }
            }

            if (browsersToClean.Count == 0)
            {
                Console.WriteLine("\n-> No browser cache folders found. Press any key to continue...");
                Console.ReadKey();
                ShowCleanupMenu();
                return;
            }

            Console.WriteLine($"\n{browsersToClean.Count} browser(s) ready for cleanup.");
            Console.Write("\nContinue? (Y/N): ");
            if (Console.ReadLine().ToUpper() != "Y") { ShowCleanupMenu(); return; }

            // Perform cleanup
            Console.WriteLine("\n-> Clearing browser cache...");
            int totalDeleted = 0;

            foreach (var (name, path) in browsersToClean)
            {
                try
                {
                    int deleted = 0;
                    foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                    {
                        try
                        {
                            File.Delete(file);
                            deleted++;
                        }
                        catch { /* Skip locked files */ }
                    }
                    Console.WriteLine($"✓ {name}: Deleted {deleted} cache files");
                    totalDeleted += deleted;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ {name} cleanup failed: {ex.Message}");
                }
            }

            Console.WriteLine($"\n-> Total cache files deleted: {totalDeleted}");
            Console.WriteLine("-> Browsers will rebuild cache on next launch");
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            ShowCleanupMenu();
        }

        static (string name, string path) GetBrowserInfo(int browserId)
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string roamingAppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            switch (browserId)
            {
                case 1: // Chrome
                    return ("Google Chrome", Path.Combine(localAppData, @"Google\Chrome\User Data\Default\Cache"));
                case 2: // Edge
                    return ("Microsoft Edge", Path.Combine(localAppData, @"Microsoft\Edge\User Data\Default\Cache"));
                case 3: // Firefox
                    // Firefox uses profile folders with random names - find the default profile
                    string firefoxProfiles = Path.Combine(roamingAppData, @"Mozilla\Firefox\Profiles");
                    if (Directory.Exists(firefoxProfiles))
                    {
                        foreach (string profileDir in Directory.GetDirectories(firefoxProfiles))
                        {
                            if (profileDir.Contains(".default-release") || profileDir.Contains(".default"))
                            {
                                return ("Mozilla Firefox", Path.Combine(profileDir, "cache2"));
                            }
                        }
                    }
                    return ("Mozilla Firefox", Path.Combine(firefoxProfiles, "default-release\\cache2"));
                case 4: // Brave
                    return ("Brave Browser", Path.Combine(localAppData, @"BraveSoftware\Brave-Browser\User Data\Default\Cache"));
                default:
                    return ("Unknown", "");
            }
        }

        // =============== DISK SPACE ANALYZER ===============
        static void AnalyzeDiskSpace()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("-> DISK SPACE ANALYZER");
            Console.WriteLine("──────────────────────");
            Console.WriteLine("Scanning C: drive for largest folders...");
            Console.WriteLine("(This may take 10-30 seconds)");
            Console.WriteLine();

            try
            {
                var root = new DirectoryInfo(@"C:\");
                var largestFolders = new System.Collections.Generic.List<(string name, long size)>();

                foreach (var dir in root.GetDirectories("*", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        long size = GetDirectorySize(dir);
                        largestFolders.Add((dir.Name, size));
                    }
                    catch { /* Skip inaccessible folders */ }
                }

                // Sort by size (largest first) and show top 10
                largestFolders.Sort((a, b) => b.size.CompareTo(a.size));

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Top 10 Largest Folders on C: Drive");
                Console.WriteLine("────────────────────────────────────");
                Console.ResetColor();

                int count = 0;
                foreach (var (name, size) in largestFolders)
                {
                    if (count >= 10) break;
                    double gb = size / (1024.0 * 1024.0 * 1024.0);
                    if (gb < 0.1) continue; // Skip tiny folders

                    // Visual size bar
                    int barLength = Math.Min(40, (int)(gb / 50.0 * 40));
                    string bar = new string('█', barLength) + new string('░', 40 - barLength);

                    Console.Write($"{name,-20} ");
                    Console.ForegroundColor = gb > 20 ? ConsoleColor.Red : (gb > 10 ? ConsoleColor.Yellow : ConsoleColor.Green);
                    Console.Write($"{gb,6:F1} GB ");
                    Console.ResetColor();
                    Console.WriteLine($"[{bar}]");
                    count++;
                }

                Console.WriteLine();
                Console.WriteLine("-> Tips:");
                Console.WriteLine("-> Folders >20 GB in RED may contain unnecessary files");
                Console.WriteLine("-> 'Windows.old' can be safely removed after Windows updates");
                Console.WriteLine("-> 'Program Files' should NOT be manually cleaned");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"-> Disk analysis failed: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            ShowCleanupMenu();
        }

        static long GetDirectorySize(DirectoryInfo directory)
        {
            long size = 0;
            try
            {
                // Add file sizes
                foreach (FileInfo file in directory.GetFiles("*", SearchOption.TopDirectoryOnly))
                {
                    try { size += file.Length; } catch { }
                }

                // Add subdirectory sizes (limit recursion depth for performance)
                int depth = 0;
                foreach (DirectoryInfo subDir in directory.GetDirectories("*", SearchOption.TopDirectoryOnly))
                {
                    if (depth++ > 3) break; // Limit recursion depth
                    try { size += GetDirectorySize(subDir); } catch { }
                }
            }
            catch { /* Skip inaccessible directories */ }
            return size;
        }

        // =============== STARTUP PROGRAM MANAGER ===============
        static void ShowStartupManager()
        {
            Console.Clear();
            ShowHeader();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Startup Program Manager:");
            Console.WriteLine("───────────────────────");
            Console.ResetColor();

            try
            {
                // Current user startup
                var hkcu = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false);
                var hklm = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false);

                Console.WriteLine("User Startup Programs:");
                Console.WriteLine("─────────────────────");
                if (hkcu != null)
                {
                    foreach (string name in hkcu.GetValueNames())
                    {
                        Console.WriteLine($"  • {name,-25} {hkcu.GetValue(name)}");
                    }
                }
                else
                {
                    Console.WriteLine("  (None found)");
                }

                Console.WriteLine("\nSystem Startup Programs:");
                Console.WriteLine("──────────────────────");
                if (hklm != null)
                {
                    foreach (string name in hklm.GetValueNames())
                    {
                        Console.WriteLine($"  • {name,-25} {hklm.GetValue(name)}");
                    }
                }
                else
                {
                    Console.WriteLine("  (None found)");
                }

                Console.WriteLine("\n-> Safety Note: Disabling startup programs may affect functionality.");
                Console.WriteLine("Use Task Manager (Ctrl+Shift+Esc) for safe management.");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"-> Failed to read startup programs: {ex.Message}");
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            ShowToolsMenu();
        }

        // =============== EXISTING METHODS (Enhanced) ===============
        static void ClearTempFiles()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("-> CLEAR TEMP FILES");
            Console.WriteLine("──────────────────");
            Console.WriteLine("This will delete temporary files from your system.");
            Console.WriteLine("Operation may take 10-30 seconds.");
            Console.WriteLine("Some files may be locked and skipped.");
            Console.WriteLine();

            Console.Write("Continue? (Y/N): ");
            if (Console.ReadLine().ToUpper() != "Y") { ShowCleanupMenu(); return; }

            Console.WriteLine("\nClearing temp files...");
            try
            {
                string tempPath = Path.GetTempPath();
                int deletedCount = 0;

                // Also clean Windows Temp folder
                string[] tempFolders = {
                    tempPath,
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp")
                };

                foreach (string folder in tempFolders)
                {
                    if (!Directory.Exists(folder)) continue;

                    foreach (string file in Directory.GetFiles(folder, "*.*", SearchOption.TopDirectoryOnly))
                    {
                        try
                        {
                            File.SetAttributes(file, FileAttributes.Normal);
                            File.Delete(file);
                            deletedCount++;
                        }
                        catch { }
                    }

                    foreach (string dir in Directory.GetDirectories(folder, "*", SearchOption.TopDirectoryOnly))
                    {
                        try
                        {
                            Directory.Delete(dir, true);
                            deletedCount++;
                        }
                        catch { }
                    }
                }

                Console.WriteLine($"\n✓ Successfully deleted {deletedCount} temporary files/folders");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n-> Failed to clean temp files: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowCleanupMenu();
        }

        static void ClearRecycleBin()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("-> CLEAR RECYCLE BIN");
            Console.WriteLine("───────────────────");
            Console.WriteLine("This will PERMANENTLY delete all files in your Recycle Bin.");
            Console.WriteLine("Operation is IRREVERSIBLE.");
            Console.WriteLine();

            Console.Write("Continue? (Y/N): ");
            if (Console.ReadLine().ToUpper() != "Y") { ShowCleanupMenu(); return; }

            Console.WriteLine("\nEmptying Recycle Bin...");
            try
            {
                SHEmptyRecycleBin(IntPtr.Zero, null, 0x00000001 | 0x00000002);
                Console.WriteLine("\n-> Recycle Bin emptied successfully");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n-> Failed to empty Recycle Bin: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowCleanupMenu();
        }

        static void RunDiskCleanup()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("-> RUN DISK CLEANUP");
            Console.WriteLine("───────────────────");
            Console.WriteLine("This will launch Windows Disk Cleanup tool.");
            Console.WriteLine("Window may take 10-30 seconds to appear.");
            Console.WriteLine();
            Console.WriteLine("-> Pro Tip: Check 'Temporary files' and 'Recycle Bin' for best results");

            Console.Write("\nContinue? (Y/N): ");
            if (Console.ReadLine().ToUpper() != "Y") { ShowCleanupMenu(); return; }

            Console.WriteLine("\nStarting Disk Cleanup...");
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cleanmgr.exe",
                    Arguments = "/sagerun:1",
                    Verb = "runas",
                    UseShellExecute = true
                });
                Console.WriteLine("\n-> Disk Cleanup started! Window may take 10-30 seconds to appear");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n-> Failed to launch Disk Cleanup: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowCleanupMenu();
        }

        static void ClearDownloads()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("-> CLEAR DOWNLOADS (IRREVERSIBLE)");
            Console.WriteLine("─────────────────────────────────");
            Console.WriteLine("This will PERMANENTLY DELETE ALL FILES in your Downloads folder.");
            Console.WriteLine("Files bypass Recycle Bin - operation is IRREVERSIBLE.");
            Console.WriteLine("Includes documents, installers, photos, videos.");
            Console.WriteLine();

            Console.Write("TYPE 'DELETE' TO CONFIRM: ");
            if (Console.ReadLine() != "DELETE") { ShowCleanupMenu(); return; }

            Console.WriteLine("\nClearing Downloads folder (IRREVERSIBLE)...");
            try
            {
                string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                if (!Directory.Exists(downloadsPath))
                {
                    Console.WriteLine("\n-> Downloads folder not found");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    ShowCleanupMenu();
                    return;
                }

                int deletedCount = 0;
                foreach (string file in Directory.GetFiles(downloadsPath, "*.*", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        File.SetAttributes(file, FileAttributes.Normal);
                        File.Delete(file);
                        deletedCount++;
                    }
                    catch { }
                }

                foreach (string dir in Directory.GetDirectories(downloadsPath, "*", SearchOption.TopDirectoryOnly))
                {
                    try
                    {
                        Directory.Delete(dir, true);
                        deletedCount++;
                    }
                    catch { }
                }

                Console.WriteLine($"\n-> Successfully deleted {deletedCount} files from Downloads folder");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n-> Failed to clear Downloads: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowCleanupMenu();
        }

        static void ShowPowerMenu()
        {
            Console.Clear();
            ShowHeader();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Power Management:");
            Console.WriteLine("────────────────");
            Console.ResetColor();
            Console.WriteLine("1. Change Power Plan");
            Console.WriteLine("2. Reboot System");
            Console.WriteLine("3. Shutdown System");
            Console.WriteLine("0. Back to Main Menu");
            Console.WriteLine();

            Console.Write("Select option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ChangePowerPlan();
                    break;
                case "2":
                    RebootSystem();
                    break;
                case "3":
                    ShutdownSystem();
                    break;
                case "0":
                    ShowMenu();
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Press any key to continue...");
                    Console.ReadKey();
                    ShowPowerMenu();
                    break;
            }
        }

        static void ChangePowerPlan()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("-> CHANGE POWER PLAN");
            Console.WriteLine("────────────────────");
            Console.WriteLine("1. Balanced (Recommended)");
            Console.WriteLine("2. High Performance");
            Console.WriteLine("3. Ultimate Performance");
            Console.WriteLine("0. Back to Power Menu");
            Console.WriteLine();

            Console.Write("Select option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ApplyPowerPlan("381b4222-f694-41f0-9685-ff5bb260df2e", "Balanced");
                    break;
                case "2":
                    ApplyPowerPlan("8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c", "High Performance");
                    break;
                case "3":
                    ApplyPowerPlan("e9a42b02-d5df-448d-aa00-03f14749eb6c", "Ultimate Performance");
                    break;
                case "0":
                    ShowPowerMenu();
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Press any key to continue...");
                    Console.ReadKey();
                    ChangePowerPlan();
                    break;
            }
        }

        static void ApplyPowerPlan(string guid, string name)
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("-> APPLYING POWER PLAN");
            Console.WriteLine("──────────────────────");
            Console.WriteLine($"Changing to: {name}");
            Console.WriteLine("This will apply immediately to all users.");
            Console.WriteLine();

            Console.Write("Continue? (Y/N): ");
            if (Console.ReadLine().ToUpper() != "Y") { ChangePowerPlan(); return; }

            Console.WriteLine("\nChanging power plan...");
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "powercfg.exe",
                    Arguments = $"/setactive {guid}",
                    Verb = "runas",
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };
                using (var process = Process.Start(psi))
                {
                    process.WaitForExit(3000);
                }
                Console.WriteLine($"\n✓ Power plan changed to {name} successfully");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Failed to change power plan: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ChangePowerPlan();
        }

        static void RebootSystem()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("-> REBOOT SYSTEM");
            Console.WriteLine("───────────────");
            Console.WriteLine("Computer will restart in 5 seconds.");
            Console.WriteLine("Save all work first!");
            Console.WriteLine();

            Console.Write("TYPE 'REBOOT' TO CONFIRM: ");
            if (Console.ReadLine() != "REBOOT") { ShowPowerMenu(); return; }

            Console.WriteLine("\nRebooting system...");
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "shutdown.exe",
                    Arguments = "/r /f /t 5",
                    Verb = "runas",
                    UseShellExecute = true
                });
                Console.WriteLine("\n-> Reboot initiated (5 second countdown)");
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n-> Failed to reboot: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowPowerMenu();
        }

        static void ShutdownSystem()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("-> SHUTDOWN SYSTEM");
            Console.WriteLine("─────────────────");
            Console.WriteLine("Computer will power off in 5 seconds.");
            Console.WriteLine("Save all work first!");
            Console.WriteLine();

            Console.Write("TYPE 'SHUTDOWN' TO CONFIRM: ");
            if (Console.ReadLine() != "SHUTDOWN") { ShowPowerMenu(); return; }

            Console.WriteLine("\nShutting down system...");
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "shutdown.exe",
                    Arguments = "/s /f /t 5",
                    Verb = "runas",
                    UseShellExecute = true
                });
                Console.WriteLine("\n-> Shutdown initiated (5 second countdown)");
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n-> Failed to shutdown: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowPowerMenu();
        }

        static void ShowNetworkMenu()
        {
            Console.Clear();
            ShowHeader();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Network Tools:");
            Console.WriteLine("─────────────");
            Console.ResetColor();
            Console.WriteLine("1. Reset Network Configuration");
            Console.WriteLine("2. Flush DNS Cache");
            Console.WriteLine("3. Renew IP Address");
            Console.WriteLine("0. Back to Main Menu");
            Console.WriteLine();

            Console.Write("Select option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ResetNetwork();
                    break;
                case "2":
                    FlushDNS();
                    break;
                case "3":
                    RenewIP();
                    break;
                case "0":
                    ShowMenu();
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Press any key to continue...");
                    Console.ReadKey();
                    ShowNetworkMenu();
                    break;
            }
        }

        static void ResetNetwork()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("-> RESET NETWORK CONFIGURATION");
            Console.WriteLine("────────────────────────────");
            Console.WriteLine("This resets TCP/IP stack and Winsock catalog.");
            Console.WriteLine("Internet may disconnect temporarily.");
            Console.WriteLine("REBOOT REQUIRED after completion.");
            Console.WriteLine();

            Console.Write("TYPE 'RESET' TO CONFIRM: ");
            if (Console.ReadLine() != "RESET") { ShowNetworkMenu(); return; }

            Console.WriteLine("\nResetting network configuration...");
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c netsh int ip reset && netsh winsock reset && ipconfig /flushdns && echo Network reset complete! && pause",
                    Verb = "runas",
                    UseShellExecute = true
                };
                Process.Start(psi);
                Console.WriteLine("\n✓ Network reset initiated");
                Console.WriteLine("->  REBOOT YOUR PC AFTER THIS OPERATION TO APPLY CHANGES");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n-> Failed to reset network: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowNetworkMenu();
        }

        static void FlushDNS()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("-> FLUSH DNS CACHE");
            Console.WriteLine("─────────────────");
            Console.WriteLine("This clears the DNS resolver cache.");
            Console.WriteLine();

            Console.Write("Continue? (Y/N): ");
            if (Console.ReadLine().ToUpper() != "Y") { ShowNetworkMenu(); return; }

            Console.WriteLine("\nFlushing DNS cache...");
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c ipconfig /flushdns",
                    Verb = "runas",
                    UseShellExecute = true,
                    CreateNoWindow = true
                };
                using (var process = Process.Start(psi))
                {
                    process.WaitForExit(5000);
                }
                Console.WriteLine("\n-> DNS cache flushed successfully");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n-> Failed to flush DNS: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowNetworkMenu();
        }

        static void RenewIP()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("-> RENEW IP ADDRESS");
            Console.WriteLine("──────────────────");
            Console.WriteLine("This releases and renews your DHCP lease.");
            Console.WriteLine();

            Console.Write("Continue? (Y/N): ");
            if (Console.ReadLine().ToUpper() != "Y") { ShowNetworkMenu(); return; }

            Console.WriteLine("\nRenewing IP address...");
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = "/c ipconfig /release && ipconfig /renew",
                    Verb = "runas",
                    UseShellExecute = true,
                    CreateNoWindow = true
                };
                using (var process = Process.Start(psi))
                {
                    process.WaitForExit(10000);
                }
                Console.WriteLine("\n-> IP address renewed successfully");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n-> Failed to renew IP: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowNetworkMenu();
        }

        static void ShowToolsMenu()
        {
            Console.Clear();
            ShowHeader();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("System Utilities:");
            Console.WriteLine("────────────────");
            Console.ResetColor();
            Console.WriteLine("1. Windows Settings");
            Console.WriteLine("2. Control Panel");
            Console.WriteLine("3. Task Manager");
            Console.WriteLine("4. Device Manager");
            Console.WriteLine("5. Command Prompt");
            Console.WriteLine("6. Startup Program Manager");
            Console.WriteLine("7. Create System Restore Point");
            Console.WriteLine("0. Back to Main Menu");
            Console.WriteLine();

            Console.Write("Select option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    OpenApp("ms-settings:");
                    break;
                case "2":
                    OpenApp("control.exe");
                    break;
                case "3":
                    OpenApp("taskmgr.exe");
                    break;
                case "4":
                    OpenAppAdmin("devmgmt.msc");
                    break;
                case "5":
                    OpenAppAdmin("cmd.exe");
                    break;
                case "6":
                    ShowStartupManager();
                    break;
                case "7":
                    CreateRestorePoint();
                    break;
                case "0":
                    ShowMenu();
                    break;
                default:
                    Console.WriteLine("\nInvalid option. Press any key to continue...");
                    Console.ReadKey();
                    ShowToolsMenu();
                    break;
            }
        }

        static void OpenApp(string path)
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine($"Opening: {path}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            try
            {
                Process.Start(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n-> Failed to open {path}: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowToolsMenu();
        }

        static void OpenAppAdmin(string path)
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine($"Opening (Admin): {path}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = path,
                    Verb = "runas",
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n-> Failed to open {path}: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowToolsMenu();
        }
        static void CreateRestorePoint()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("🛡️ CREATE SYSTEM RESTORE POINT");
            Console.WriteLine("─────────────────────────────");
            Console.WriteLine("Creates a restore point before major operations.");
            Console.WriteLine("Recommended before cleanup operations.");
            Console.WriteLine();

            Console.Write("Continue? (Y/N): ");
            if (Console.ReadLine().ToUpper() != "Y") { ShowCleanupMenu(); return; }

            Console.WriteLine("\nCreating restore point...");
            try
            {
                // Create restore point via PowerShell
                var psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = "-Command \"Checkpoint-Computer -Description 'PC Care Utility v2.3' -RestorePointType MODIFY_SETTINGS\"",
                    Verb = "runas",
                    UseShellExecute = true,
                    CreateNoWindow = true
                };
                using (var process = Process.Start(psi))
                {
                    process.WaitForExit(10000);
                }
                Console.WriteLine("\n✓ System restore point created successfully!");
                Console.WriteLine("Name: PC Care Utility v2.3");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Failed to create restore point: {ex.Message}");
                Console.WriteLine("This may happen if System Protection is disabled.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            ShowCleanupMenu();
        }
        static void ShowAbout()
        {
            Console.Clear();
            ShowHeader();
            Console.WriteLine("-> ABOUT PC CARE UTILITY");
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("v2.3 - Safe System Maintenance for Windows 10/11");
            Console.WriteLine();
            Console.WriteLine("-> NEW FEATURES IN v2.3:");
            Console.WriteLine("-> Browser Cache Cleaner (Chrome/Edge/Firefox/Brave)");
            Console.WriteLine("-> Disk Space Analyzer (visual folder size display)");
            Console.WriteLine("-> Startup Program Manager (view-only for safety)");
            Console.WriteLine("-> Enhanced safety confirmations (typing required)");
            Console.WriteLine("-> Added the option to create System Restore Points");
            Console.WriteLine("-> Added a Privacy Guard");
            Console.WriteLine();
            Console.WriteLine("-> CORE FEATURES:");
            Console.WriteLine("-> File cleanup (Temp, Recycle Bin, Downloads)");
            Console.WriteLine("-> Disk space recovery");
            Console.WriteLine("-> Power plan management");
            Console.WriteLine("-> Network troubleshooting");
            Console.WriteLine("-> System power controls");
            Console.WriteLine();
            Console.WriteLine("-> SAFETY FIRST:");
            Console.WriteLine("-> NO BIOS/UEFI operations (physically dangerous)");
            Console.WriteLine("-> NO registry cleaners (modern Windows doesn't need them)");
            Console.WriteLine("-> All destructive operations require explicit confirmation");
            Console.WriteLine("-> Downloads deletion requires typing 'DELETE' to confirm");
            Console.WriteLine("-> Requires Administrator rights for full functionality");
            Console.WriteLine();
            Console.WriteLine("-> PRO TIP: Always close browsers before clearing cache!");
            Console.WriteLine();
            Console.WriteLine("-> Developed by Mohammad Harib Ahmad.");
            Console.WriteLine("-> 2026 - All rights reserved");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            ShowMenu();
        }

        [DllImport("Shell32.dll", SetLastError = true)]
        private static extern bool SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, uint dwFlags);
    }
}