<?php
namespace Cornerstone\Quarry\Spec;
use Cornerstone\Quarry\Targets\RawForm;

class Compile
{
    function cli($args, $config)
    {
        if ($args) {
            $files = [$args[0]];
        } else {
            $files = glob("../spec/*.json");
        }

        foreach ($files as $fname) {
            $f = json_decode(file_get_contents($fname), 1);
            $handle = fopen(
                "work/" .
                pathinfo($fname)['filename'] .
                "." . RawForm::name .
                ".request"
            , 'w');
            RawForm::convert($f, $config, $handle);
            fclose($handle);
        }
    }
}
