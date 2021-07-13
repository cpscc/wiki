<?php
namespace Cornerstone\Quarry\Targets;

class RawForm extends Raw
{
    const name = "raw.csv";
    const ctype = "application/csv";

    function convert(array $spec, array $config, $h)
    {
        parent::convert($spec, $config, $h);
    }
}

