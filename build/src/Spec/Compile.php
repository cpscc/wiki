<?php
namespace Cornerstone\Quarry\Spec;
use Cornerstone\Quarry\Targets\RawForm;

class Compile
{
    function cli($args)
    {
        if ($args) {
            $files = [$args[0]];
        } else {
            $files = glob("../spec/*.json");
        }

        foreach ($files as $f) {
            $f = json_decode(file_get_contents($f), 1);
            RawForm::convert($f);
        }
    }
}
