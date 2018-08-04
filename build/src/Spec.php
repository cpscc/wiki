<?php
namespace Cornerstone\Quarry;

class Spec
{
    function cli($argc, $argv)
    {
        if ($argc < 2) {
            echo "ERROR: at least 1 argument is required\n";
            exit(1);
        }

        $class = 'Cornerstone\\Quarry\\Spec\\' . ucwords($argv[1]);
        if (class_exists($class)) {
            (new $class)->cli($argc, $argv);
        } else {
            echo "ERROR: unknown sub-command \"$argv[1]\"\n";
            exit;
        }
    }
}
