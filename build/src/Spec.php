<?php
namespace Cornerstone\Quarry;

class Spec
{
    function cli($argc, $argv)
    {
        $script = array_shift($argv);
        $command = array_shift($argv);
        $config = parse_ini_file('../defaults.ini');
        // TODO: replace defaults with ENV if present

        if ($argc < 2) {
            echo "ERROR: at least 1 argument is required\n";
            exit(1);
        }

        $class = 'Cornerstone\\Quarry\\Spec\\' . ucwords($command);
        if (class_exists($class)) {
            (new $class)->cli($argv, $config);
        } else {
            echo "ERROR: unknown sub-command \"$argv[1]\"\n";
            exit;
        }
    }
}
